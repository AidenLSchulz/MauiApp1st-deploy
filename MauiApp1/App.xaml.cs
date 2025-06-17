using System.Runtime.Versioning;   // For SupportedOSPlatformAttribute
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    /// <summary>
    /// The Application class is the “root” of your MAUI app.
    /// - InitializeComponent() loads App.xaml resources (styles, merged dictionaries, converters, etc.).
    /// - MainPage assigns the shell or first page (AppShell).
    ///
    /// We decorate with [SupportedOSPlatform] so the analyzer knows this code is valid on all MAUI targets.
    /// </summary>
    [SupportedOSPlatform("android")]
    [SupportedOSPlatform("ios")]
    [SupportedOSPlatform("windows10.0.19041.0")]
    [SupportedOSPlatform("maccatalyst")]
    public partial class App : Application
    {
        public App()
        {
            // Load XAML-defined resources (App.xaml)
            InitializeComponent();

            // Set the “root” page to AppShell, which manages navigation/flyout/tabs
            MainPage = new AppShell();
        }
    }
}
