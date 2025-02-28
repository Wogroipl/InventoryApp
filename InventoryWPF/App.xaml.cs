using System.Windows;
using InventoryWPF.Factories;
using InventoryWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Core;
using Repository.DataAccess;

namespace InventoryWPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);

        var configuration = builder.Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });

        services.AddTransient<MainViewModel>();
        services.AddTransient<CustomersViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<InventoryViewModel>();
        services.AddTransient<JobViewModel>();
        services.AddTransient<VenuesViewModel>();

        //register viewmodelfactory
        services.AddSingleton<Func<PageType, ViewModelBase>>(serviceProvider => pageType =>
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
        services.AddSingleton<PageFactory>();


        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}

