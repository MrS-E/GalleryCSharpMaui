using Microsoft.Extensions.Logging;

namespace Gallery
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

#if DEBUG
            builder.Logging.AddDebug();
#endif
            //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ImagesDB>(s, Path.Combine(FileSystem.AppDataDirectory, "data.db")));

            return builder.Build();
        }
    }
}