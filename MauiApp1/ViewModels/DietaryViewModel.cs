// ===============================
// 📁 DietaryViewModel.cs
// Purpose: ViewModel for tracking food log
// ===============================
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class DietaryViewModel : INotifyPropertyChanged
    {
        // Stores user-entered food entries
        public ObservableCollection<FoodEntry> FoodLog { get; } = new();

        // Bound to form inputs
        private string description = string.Empty;
        public string Description { get => description; set { SetProperty(ref description, value); OnPropertyChanged(nameof(CanAddFood)); } }

        private int calories;
        public int Calories { get => calories; set { SetProperty(ref calories, value); OnPropertyChanged(nameof(CanAddFood)); } }

        private int protein;
        public int Protein { get => protein; set => SetProperty(ref protein, value); }

        private int carbs;
        public int Carbs { get => carbs; set => SetProperty(ref carbs, value); }

        private int fats;
        public int Fats { get => fats; set => SetProperty(ref fats, value); }

        // Commands for Add/Delete/toggle expand
        public ICommand AddFoodCommand { get; }
        public ICommand DeleteFoodCommand { get; }
        public ICommand ToggleNutritionCommand { get; }

        public DietaryViewModel()
        {
            AddFoodCommand = new Command(AddFood, () => CanAddFood);
            DeleteFoodCommand = new Command<FoodEntry>(e => FoodLog.Remove(e));
            ToggleNutritionCommand = new Command<FoodEntry>(ToggleExpand);

            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName is nameof(Description) or nameof(Calories))
                    ((Command)AddFoodCommand).ChangeCanExecute();
            };
        }

        // Only allow adding food if description and calories are valid
        public bool CanAddFood => !string.IsNullOrWhiteSpace(Description) && Calories > 0;

        private void AddFood()
        {
            FoodLog.Add(new FoodEntry
            {
                Description = Description.Trim(),
                Calories = Calories,
                Protein = Protein,
                Carbs = Carbs,
                Fats = Fats,
                Date = DateTime.Now
            });

            // Reset form
            Description = "";
            Calories = Protein = Carbs = Fats = 0;

            // Refresh summary label
            OnPropertyChanged(nameof(TotalCaloriesToday));
        }

        private void ToggleExpand(FoodEntry entry)
        {
            entry.IsExpanded = !entry.IsExpanded;
        }

        // Total calories eaten today (for daily summary)
        public int TotalCaloriesToday =>
            FoodLog.Where(f => f.Date.Date == DateTime.Today).Sum(f => f.Calories);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = null!)
        {
            if (Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null!) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}