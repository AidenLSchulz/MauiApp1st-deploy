// ===============================
// 📁 WorkoutEntry.cs
// Purpose: Represents a workout session for the log
// ===============================
using System;

namespace MauiApp1.Models
{
    public class WorkoutEntry
    {
        public string Type { get; set; } = string.Empty;           // e.g., Running, Swimming
        public int DurationMinutes { get; set; }                   // How long the session lasted
        public int CaloriesBurned { get; set; }                    // Calories burned estimate
        public string Intensity { get; set; } = string.Empty;      // Light / Moderate / Intense

        public bool IsRecurring { get; set; }                      // Whether it's a daily recurring workout
        public TimeSpan ReminderTime { get; set; }                 // What time to remind

        public DateTime Date { get; set; } = DateTime.Now;         // When the workout was logged

        public bool IsCompleted { get; set; }                      // True if user marked it done
    }
}
