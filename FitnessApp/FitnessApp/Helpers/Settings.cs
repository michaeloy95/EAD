// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;

namespace FitnessApp.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

        private static readonly string StepFile = "step";
        private const string StartDateKey = "StartDate";

		#endregion


		public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}
        public static string StartDateStr
        {
            get
            {
                return StartDate.ToString("d");
            }
            set
            {
            }
        }
        public static DateTime StartDate
        {
            get
            {
                return AppSettings.GetValueOrDefault(StartDateKey, DateTime.MinValue, StepFile);
            }
            set
            {
                if (StartDate == DateTime.MinValue)
                    AppSettings.AddOrUpdateValue(StartDateKey, value, StepFile);
            }
        }
        public static System.Int64 StepsToday
        {
            get
            {
                return AppSettings.GetValueOrDefault(DateTime.Now.ToString("d"), 0, StepFile);
            }
            set
            {
                AppSettings.AddOrUpdateValue(DateTime.Now.ToString("d"), value, StepFile);
            }
        }
        /// <summary>
        /// Gets the dictionary of steps entry data.
        /// </summary>
        /// <param name="shortDateString">Max date string of the range in short date format (can be obtained with dateTime.ToString("d") or dateTime.ToShortDateString().</param>
        public static Dictionary<DateTime, Int64> GetStepsCountToDate(string shortDateString)
        {
            DateTime entryStartDate = AppSettings.GetValueOrDefault("StartDate", DateTime.MinValue, StepFile);
            if (entryStartDate == DateTime.MinValue)
                return null;
            else
            {
                Dictionary<DateTime, Int64> result = new Dictionary<DateTime, Int64>();
                for (var dt = entryStartDate; dt <= DateTime.Parse(shortDateString); dt = dt.AddDays(1))
                {
                    result.Add(dt, AppSettings.GetValueOrDefault(dt.ToString("d"), 0, StepFile));
                }
                return result;
            }
        }
        /// <summary>
        /// Gets the dictionary of steps entry data.
        /// </summary>
        /// <param name="startDateString">start date string in short date format (can be obtained with dateTime.ToString("d") or dateTime.ToShortDateString().</param>
        /// <param name="endDateString">end date string in short date format (can be obtained with dateTime.ToString("d") or dateTime.ToShortDateString().</param>
        public static Dictionary<DateTime, Int64> GetStepsCountInRange(string startDateString, string endDateString)
        {
            var entryStartDate = AppSettings.GetValueOrDefault("StartDate", DateTime.MinValue, StepFile);
            if (entryStartDate == DateTime.MinValue)
                return null;
            else
            {
                var startDate = DateTime.Parse(startDateString);
                Dictionary<DateTime, Int64> result = new Dictionary<DateTime, Int64>();
                for (var dt = startDate; dt <= DateTime.Parse(endDateString); dt = dt.AddDays(1))
                {
                    result.Add(dt, AppSettings.GetValueOrDefault(dt.ToString("d"), 0, StepFile));
                }
                return result;
            }
        }
    }
}