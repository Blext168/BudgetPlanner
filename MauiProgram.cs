using BudgetPlanner.Interfaces;
using BudgetPlanner.Manager;
using Microsoft.Extensions.Logging;

namespace BudgetPlanner
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

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            // Service registrieren (plattformabhängig)
#if WINDOWS
            builder.Services.AddSingleton<IBiometricService, BudgetPlanner.Platforms.Windows.BiometricService>();
#elif ANDROID
            builder.Services.AddSingleton<IBiometricService, BudgetPlanner.Platforms.Android.BiometricService>();
#elif IOS
            builder.Services.AddSingleton<IBiometricService, BudgetPlanner.Platforms.iOS.BiometricService>();
#else
            builder.Services.AddSingleton<IBiometricService, BudgetPlanner.Classes.DummyBiometricService>();
#endif

            builder.Services.AddScoped<IExpenseManager, ExpenseManager>();
            builder.Services.AddScoped<IVehicleManager, VehicleManager>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            
            return builder.Build();
        }
    }
}
