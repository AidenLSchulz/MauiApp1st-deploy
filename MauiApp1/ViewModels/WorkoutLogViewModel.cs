// ===============================
// 📁 WorkoutLogViewModel.cs
// Purpose: ViewModel for MainPage (Workout logging)
// ===============================
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiApp1.Models;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    public class WorkoutLogViewModel : INotifyPropertyChanged
    {
        // ================= Properties bound to form inputs =================
        private string type = string.Empty;
        private int duration = 0;
        private int calories = 0;
        private string intensity = string.Empty;
        private bool isRecurring = false;
        private TimeSpan reminderTime = TimeSpan.Zero;

        public string Type { get => type; set => SetProperty(ref type, value); }
        public int Duration { get => duration; set => SetProperty(ref duration, value); }
        public int Calories { get => calories; set => SetProperty(ref calories, value); }
        public string Intensity { get => intensity; set => SetProperty(ref intensity, value); }
        public bool IsRecurring { get => isRecurring; set => SetProperty(ref isRecurring, value); }
        public TimeSpan ReminderTime { get => reminderTime; set => SetProperty(ref reminderTime, value); }

        // Collection to store all workouts
        public ObservableCollection<WorkoutEntry> Workouts { get; } = new();

        // Commands bound to buttons and actions
        public ICommand AddWorkoutCommand { get; }
        public ICommand DeleteWorkoutCommand { get; }
        public ICommand LogWorkoutCommand { get; } // Optional: mark workout as completed

        // Event to notify page (MainPage) that a workout was added
        public event Action? WorkoutAdded;
        public static event EventHandler? WorkoutLoggedEvent; // Broadcast event (not used yet)

        public WorkoutLogViewModel()
        {
            AddWorkoutCommand = new Command(OnAddWorkout, CanAddWorkout);
            DeleteWorkoutCommand = new Command<WorkoutEntry>(e => Workouts.Remove(e));
            LogWorkoutCommand = new Command<WorkoutEntry>(OnLogWorkout);

            // When form values change, check if AddWorkoutCommand should be enabled
            PropertyChanged += (_, e) =>
            {
                if (e.PropertyName is nameof(Type) or nameof(Duration) or nameof(Calories) or nameof(Intensity))
                {
                    ((Command)AddWorkoutCommand).ChangeCanExecute();
                }
            };
        }

        private bool CanAddWorkout() =>
            !string.IsNullOrWhiteSpace(Type) && Duration > 0 && Calories >= 0 && !string.IsNullOrWhiteSpace(Intensity);

        private void OnAddWorkout()
        {
            // Add new workout to log
            var entry = new WorkoutEntry
            {
                Type = Type.Trim(),
                DurationMinutes = Duration,
                CaloriesBurned = Calories,
                Intensity = Intensity.Trim(),
                IsRecurring = IsRecurring,
                ReminderTime = ReminderTime,
                Date = DateTime.Now
            };
            Workouts.Add(entry);

            // Launch reminder based on IsRecurring
            _ = entry.IsRecurring ? RepeatReminderAsync(entry) : OneTimeReminderAsync(entry);

            // Reset input fields
            Type = "";
            Duration = 0;
            Calories = 0;
            Intensity = "";
            IsRecurring = false;
            ReminderTime = TimeSpan.Zero;

            // Trigger UI alert (MainPage listens to this)
            WorkoutAdded?.Invoke();
        }

        private void OnLogWorkout(WorkoutEntry entry)
        {
            entry.IsCompleted = true;
            WorkoutLoggedEvent?.Invoke(this, EventArgs.Empty);
        }

        // Waits until the desired time and shows a one-time alert
        private async Task OneTimeReminderAsync(WorkoutEntry entry)
        {
            var now = DateTime.Now;
            var next = now.Date + entry.ReminderTime;
            if (next < now) next = next.AddDays(1);
            await Task.Delay(next - now);
            await Application.Current!.MainPage!.DisplayAlert("Reminder", $"Time for: {entry.Type}", "OK");
        }

        // Daily recurring reminder alert loop
        private async Task RepeatReminderAsync(WorkoutEntry entry)
        {
            while (true)
            {
                var now = DateTime.Now;
                var next = now.Date + entry.ReminderTime;
                if (next < now) next = next.AddDays(1);
                await Task.Delay(next - now);
                await Application.Current!.MainPage!.DisplayAlert("Reminder", $"Daily workout: {entry.Type}", "OK");
            }
        }

        // Required for INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string name = null!)
        {
            if (Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            return true;
        }
    }
}