// ===============================
// 📁 FoodEntry.cs
// Purpose: Model representing a single food log entry
// ===============================
using System;
using System.ComponentModel;

namespace MauiApp1
{
    public class FoodEntry : INotifyPropertyChanged
    {
        // Basic info
        public string Description { get; set; } = string.Empty;
        public int Calories { get; set; }

        // Nutrients
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fats { get; set; }

        // Timestamp of entry
        public DateTime Date { get; set; } = DateTime.Now;

        // Tracks whether this entry is expanded in UI
        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (isExpanded == value) return;
                isExpanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
