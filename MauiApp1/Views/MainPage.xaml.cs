// =======================================
// 📄 MainPage.xaml.cs (code-behind)
// Purpose: Connects MainPage.xaml UI to ViewModel
// =======================================
using System.Runtime.Versioning;
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    /// <summary>
    /// Code-behind for MainPage:
    /// 1️⃣ Instantiates WorkoutLogViewModel and sets BindingContext.
    /// 2️⃣ Subscribes to WorkoutAdded to display a success alert.
    /// </summary>

    // 🛡️ Restricts execution to supported OS platforms
    [SupportedOSPlatform("android21.0")]
    [SupportedOSPlatform("ios13.0")]
    [SupportedOSPlatform("windows10.0.19041.0")]
    [SupportedOSPlatform("maccatalyst")]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent(); // 🔧 Load XAML UI

            // 1️⃣ Create and attach the ViewModel
            var vm = new WorkoutLogViewModel();
            BindingContext = vm;

            // 2️⃣ Subscribe to custom event for success feedback
            vm.WorkoutAdded += async () =>
            {
                // ✅ Show confirmation alert when workout is added
                await DisplayAlert("Success", "Workout added!", "OK");
            };
        }
    }
}