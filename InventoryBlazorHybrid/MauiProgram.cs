using InventoryBlazorHybrid.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;

namespace InventoryBlazorHybrid
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Add configuration
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: false)
                .AddUserSecrets<MauiApp>(optional: true, reloadOnChange: true);

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            //Add DBContext
            builder.Services.AddDbContext<InventoryDbContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Register the services
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddFluentUIComponents();
            builder.Services.AddDataGridEntityFrameworkAdapter();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();

#endif

            return builder.Build();
        }
    }
}
