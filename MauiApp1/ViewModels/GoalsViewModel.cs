// ===============================
// 📁 GoalsViewModel.cs
// Purpose: ViewModel for setting and tracking fitness goals
// ===============================
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Windows.Input;
using MauiApp1.Models;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    [SupportedOSPlatform("android21.0")]
    [SupportedOSPlatform("ios13.0")]
    [SupportedOSPlatform("windows10.0.19041.0")]
    [SupportedOSPlatform("maccatalyst")]
    public class GoalsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GoalEntry> Goals { get; } = new();

        // Bound to form inputs
        private string description = string.Empty;
        public string Description { get => description; set => SetProperty(ref description, value); }

        private double targetValue;
        public double TargetValue { get => targetValue; set => SetProperty(ref targetValue, value); }

        private string unit = string.Empty;
        public string Unit { get => unit; set => SetProperty(ref unit, value); }

        // Commands for goal actions
        public ICommand AddGoalCommand { get; }
        public ICommand DeleteGoalCommand { get; }
        public ICommand IncreaseProgressCmd { get; }
        public ICommand DecreaseProgressCmd { get; }

        public GoalsViewModel()
        {
            AddGoalCommand = new Command(AddGoal, CanAddGoal);
            DeleteGoalCommand = new Command<GoalEntry>(g => Goals.Remove(g));
            IncreaseProgressCmd = new Command<GoalEntry>(OnIncrease);
            DecreaseProgressCmd = new Command<GoalEntry>(OnDecrease);

            PropertyChanged += (s, e) =>
            {
                if (e.PropertyName is nameof(Description) or nameof(TargetValue) or nameof(Unit))
                {
                    ((Command)AddGoalCommand).ChangeCanExecute();
                }
            };
        }

        private bool CanAddGoal() =>
            !string.IsNullOrWhiteSpace(Description) && TargetValue > 0 && !string.IsNullOrWhiteSpace(Unit);

        private void AddGoal()
        {
            Goals.Add(new GoalEntry
            {
                Description = Description.Trim(),
                TargetValue = TargetValue,
                CurrentValue = 0,
                Unit = Unit.Trim()
            });

            // Reset fields
            Description = "";
            TargetValue = 0;
            Unit = "";
        }

        private void OnIncrease(GoalEntry goal)
        {
            if (goal.CurrentValue < goal.TargetValue)
            {
                goal.CurrentValue++;
                UpdateProgressColor(goal);
            }
        }

        private void OnDecrease(GoalEntry goal)
        {
            if (goal.CurrentValue > 0)
            {
                goal.CurrentValue--;
                UpdateProgressColor(goal);
            }
        }

        // Dynamically change the ProgressBar color based on % complete
        private void UpdateProgressColor(GoalEntry goal)
        {
            double progress = goal.CurrentValue / goal.TargetValue;

            // Null-safe: only try to update the resource if the app is fully loaded
            if (Application.Current?.Resources != null)
            {
                Application.Current.Resources["ProgressBarColor"] =
                    progress >= 0.8 ? Colors.Green :
                    progress >= 0.5 ? Colors.Orange :
                    Colors.Red;
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propName = null!)
        {
            if (Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            return true;
        }
    }
}
