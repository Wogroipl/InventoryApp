using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Repository.DataAccess;
using Repository.Models;
using Repository.ViewModels;

namespace InventoryBlazorHybrid
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
            builder.Services.AddFluentUIComponents();
            builder.Services.AddDataGridEntityFrameworkAdapter();


            builder.Services.AddSingleton<HomeViewModel>();

            builder.Services.AddSingleton<Inventory>();

            builder.Services.AddTransient<DataService<Job>>();
            builder.Services.AddTransient<DataService<Customer>>();
            builder.Services.AddTransient<DataService<Venue>>();
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
