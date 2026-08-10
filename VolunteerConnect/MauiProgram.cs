using Microsoft.Extensions.Logging;
using VolunteerConnect.Services;
using VolunteerConnect.Views;

namespace VolunteerConnect
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "volunteer.db3");

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<OpportunitiesPage>();
            builder.Services.AddTransient<OpportunityDetailsPage>();
            builder.Services.AddTransient<RegistrationPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
