using Microsoft.UI.Xaml;
using System.Runtime.Versioning;   // for SupportedOSPlatformAttribute

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiApp1.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// This class is only used on Windows, so we annotate it as SupportedOSPlatform("windows10.0.19041.0").
    /// </summary>
    [SupportedOSPlatform("windows10.0.19041.0")]
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed on Windows, equivalent to WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
