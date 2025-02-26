using System;
using InventoryWinUI.DataAccess;
using InventoryWinUI.Factories;
using InventoryWinUI.ViewModels;
using InventoryWinUI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Repository.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace InventoryWinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            InitializeComponent();
        }




        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The one servicecollection</param>
        private void ConfigureServices(IServiceCollection services)
        {
            //Add configuration
            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            //register configuration
            services.AddSingleton<IConfiguration>(config);

            //Add DBContext
            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            //Register MainWindow
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainView>();

            //Register viewmodels as Transient
            services.AddTransient<MainViewModel>();
            services.AddTransient<HomeViewModel>();

            //register viewmodelfactory
            services.AddSingleton<Func<PageType, ViewModelBase>>(serviceProvider => pageType =>
            {
                return pageType switch
                {
                    PageType.Main => serviceProvider.GetRequiredService<MainViewModel>(),
                    PageType.Home => serviceProvider.GetRequiredService<HomeViewModel>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(pageType), pageType, null)
                };
            });
            services.AddSingleton<PageFactory>();


            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            mainWindow = _serviceProvider!.GetRequiredService<MainWindow>();
            var mainView = _serviceProvider!.GetRequiredService<MainView>();
            mainWindow.Content = mainView;
            mainWindow.Activate();
        }

        private Window? mainWindow;
    }
}
