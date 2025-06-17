// =======================================
// 📄 DietaryPage.xaml.cs (code-behind)
// Purpose: Connects DietaryPage.xaml UI to ViewModel
// =======================================
using MauiApp1.ViewModels;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class DietaryPage : ContentPage
    {
        public DietaryPage()
        {
            InitializeComponent(); // 🔧 Loads and links the XAML layout

            // 🎯 Assigns the ViewModel to handle logic + data binding
            BindingContext = new DietaryViewModel();
        }
    }
}