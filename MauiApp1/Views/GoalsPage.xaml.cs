// =======================================
// 📄 GoalsPage.xaml.cs (code-behind)
// Purpose: Connects GoalsPage.xaml UI to ViewModel
// =======================================
using System.Runtime.Versioning;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    // 🛡️ Platform restriction attributes — only run this on supported OS versions
    [SupportedOSPlatform("android21.0")]
    [SupportedOSPlatform("ios13.0")]
    [SupportedOSPlatform("windows10.0.19041.0")]
    [SupportedOSPlatform("maccatalyst")]
    public partial class GoalsPage : ContentPage
    {
        public GoalsPage()
        {
            InitializeComponent(); // 🔧 Load XAML UI

            // 🎯 Assign GoalsViewModel to provide data and commands
            BindingContext = new GoalsViewModel();
        }
    }
}
