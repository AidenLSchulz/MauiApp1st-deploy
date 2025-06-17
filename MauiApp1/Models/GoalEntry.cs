// ===============================
// 📁 GoalEntry.cs
// Purpose: Model for a user fitness goal with progress tracking
// ===============================
using System;
using System.ComponentModel;

namespace MauiApp1.Models
{
    /// <summary>
    /// Represents one fitness goal.
    /// Notifies UI when any property changes (including ProgressRatio).
    /// </summary>
    public class GoalEntry : INotifyPropertyChanged
    {
        // ─── Backing fields ──────────────────────────────────────────────
        private string description = string.Empty;
        private double targetValue;
        private double currentValue;
        private string unit = string.Empty;

        // ─── Public properties ────────────────────────────────────────────

        public string Description
        {
            get => description;
            set
            {
                if (description == value) return;
                description = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
            }
        }

        public double TargetValue
        {
            get => targetValue;
            set
            {
                if (Math.Abs(targetValue - value) < 0.001) return;
                targetValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TargetValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressRatio)));
            }
        }

        public double CurrentValue
        {
            get => currentValue;
            set
            {
                if (Math.Abs(currentValue - value) < 0.001) return;
                currentValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentValue)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressRatio)));
            }
        }

        public string Unit
        {
            get => unit;
            set
            {
                if (unit == value) return;
                unit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Unit)));
            }
        }

        // Computed value for the progress bar (between 0.0 and 1.0)
        public double ProgressRatio =>
            TargetValue > 0
                ? Math.Min(1.0, Math.Max(0.0, CurrentValue / TargetValue))
                : 0.0;

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
