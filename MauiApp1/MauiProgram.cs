using System.Runtime.Versioning;       // Used to annotate platforms like Android/iOS
using Microsoft.Extensions.Logging;    // Enables logging/debug output
using Microsoft.Maui;                  // MAUI app builder
using Microsoft.Maui.Hosting;          // Hosts the application

namespace MauiApp1
{
    /// <summary>
    /// MauiProgram is the startup class that builds and configures the app.
    /// It's called before the app launches.
    /// </summary>

    // ✅ This class is marked with SupportedOSPlatform attributes
    // to avoid warnings when calling platform-specific methods (like .UseMauiApp)
    [SupportedOSPlatform("android")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("windows10.0.19041.0")]
    [SupportedOSPlatform("maccatalyst")]
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // 🔨 Create a new MAUI app builder instance
            var builder = MauiApp.CreateBuilder();

            // 📦 Register the main App.xaml as the entry point
            builder.UseMauiApp<App>()

                   // 🔤 Add fonts that can be used globally in the app
                   .ConfigureFonts(fonts =>
                   {
                       fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                       fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                   });

#if DEBUG
            // 🐞 Enable console logging for debugging during development
            builder.Logging.AddDebug();
#endif

            // 🚀 Return the fully built MAUI app
            return builder.Build();
        }
    }
}
