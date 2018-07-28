using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp.Interfaces
{
    public interface IDeviceUtility
    {
        /// <summary>
        /// Gets the name of the device.
        /// </summary>
        /// <value>The name of the device.</value>
        string DeviceName { get; }

        /// <summary>
        /// Gets the application name.
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        string AppVersion { get; }

        /// <summary>
        /// Gets the file version.
        /// </summary>
        string FileVersion { get; }

        /// <summary>
        /// Gets the build version.
        /// </summary>
        string BuildVersion { get; }

        /// <summary>
        /// Gets the copyright string.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Gets the current application path.
        /// </summary>
        string AppPath { get; }

        /// <summary>
        /// Gets the current device status bar height.
        /// </summary>
        double StatusBarHeight { get; }

        ///<summary>
        /// Gets if the device can vibrate
        ///</summary>
        bool CanVibrate { get; }

        /// <summary>
        /// Capture current active screen.
        /// </summary>
        /// <returns>The captured active screen image filename.</returns>
        string CaptureScreen();

        /// <summary>
        /// Writes the log file.
        /// </summary>
        /// <param name="tag">Tag.</param>
        /// <param name="message">Message.</param>
        void WriteLogFile(string tag, string message);

        /// <summary>
        /// Opens the log file.
        /// </summary>
        void OpenLogFile();

        /// <summary>
        /// Deletes the log file.
        /// </summary>
        void DeleteLogFile();

        /// <summary>
        /// Exports the log file.
        /// </summary>
        void ExportLogFile();

        /// <summary>
        /// Exports the log file.
        /// </summary>
        /// <param name="stream">The output I/O stream.</param>
        void ExportLogFile(Stream stream);

        /// <summary>
        /// Reports the error to raygun.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="message">Message.</param>
        /// <param name="message">Error message.</param>
        /// <param name="data">Data.</param>
        void ReportErrorToRaygun(string action, string message, string errorMessage, object data);

        ///<summary>
        /// Vibrate the device for the time duration
        ///</summary>
        ///<param name="vibrateSpan"> Vibration time span. Default 500 ms</param>
        void Vibration(TimeSpan? vibrateSpan = null);

        ///<summary>
        /// Check OS and device compatible with step counter/pedometer sensor
        ///</summary>
        ///<param name="pm"> Android package manager</param>
        bool IsKitKatWithStepCounter(Object pm);
        bool AndroidStepSupport { get; }
    }
}
