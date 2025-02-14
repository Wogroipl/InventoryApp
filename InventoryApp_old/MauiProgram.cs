using DataAccess;
using DataModels.Models;
using InventoryApp.ViewModels;
using Microsoft.Extensions.Logging;
using Location = DataModels.Models.Location;

namespace InventoryApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<Inventory>();

            builder.Services.AddTransient<DataService<Job>>();
            builder.Services.AddTransient<DataService<Customer>>();
            builder.Services.AddTransient<DataService<Location>>();
            builder.Services.AddTransient<DataService<Product>>();
            builder.Services.AddTransient<DataService<Transaction>>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
