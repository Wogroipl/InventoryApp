using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using InventoryAppAvalonia.Factories;
using InventoryAppAvalonia.ViewModels;
using InventoryAppAvalonia.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using Repository.DataAccess;

namespace InventoryAppAvalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            //Configure services
            var collection = new ServiceCollection();
            //register services


            //Add DBContext
            collection.AddDbContext<InventoryDbContext>(options =>
                                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //register view models
            collection.AddTransient<MainWindowViewModel>();
            collection.AddTransient<HomeViewModel>();
            collection.AddTransient<JobViewModel>();
            collection.AddTransient<InventoryViewModel>();
            collection.AddTransient<CustomersViewModel>();
            collection.AddTransient<VenuesViewModel>();

            //register viewmodelfactory
            collection.AddSingleton<Func<PageType, ViewModelBase>>(serviceProvider => pageType =>
            {
                return pageType switch
                {
                    PageType.Home => serviceProvider.GetRequiredService<HomeViewModel>(),
                    PageType.Job => serviceProvider.GetRequiredService<JobViewModel>(),
                    PageType.Inventory => serviceProvider.GetRequiredService<InventoryViewModel>(),
                    PageType.Customers => serviceProvider.GetRequiredService<CustomersViewModel>(),
                    PageType.Venues => serviceProvider.GetRequiredService<VenuesViewModel>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null)
                };
            });
            collection.AddSingleton<PageFactory>();

            //build service provider
            var services = collection.BuildServiceProvider();

            desktop.MainWindow = new MainWindow
            {
                DataContext = services.GetRequiredService<MainWindowViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}