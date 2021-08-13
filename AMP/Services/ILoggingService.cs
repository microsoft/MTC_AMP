using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace AMP.Services
{
    public interface ILogging
    {

        bool IsEnabled { get; set; }
        void Information(string message);
        void Debug(string message);
        void Warning(string message);
        void Error(string message);
    }

    public class LocalFileLogger : ILogging
    {
       
        static bool _isEnabled = true;

        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }

        public void Information(string message)
        {
            if (IsEnabled)
                LogUtil.Information(message);
        }
        public void Debug(string message)
        {
            if (IsEnabled)
                LogUtil.Debug(message);
        }

        public void Warning(string message)
        {
            if (IsEnabled)
                LogUtil.Warning(message);
        }

        public void Error(string message)
        {
            if (IsEnabled)
                LogUtil.Error(message);
        }
    }

    public static class LogUtil
    {
        static string LOG_BASE_NAME = "log";

        static LogUtil()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, $"{LOG_BASE_NAME}.txt");
            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day).CreateLogger();
        }

        public static void Information(string message)
        {

            Log.Information(message);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);


        }
        public static void Warning(string message)
        {
            Log.Warning(message);
        }
        public static void Error(string message)
        {
            Log.Error(message);
        }

        public static async Task<RandomAccessStreamReference> GetFileStreamReference()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, $"{LOG_BASE_NAME}{DateTime.Today.ToString("yyyyMMdd")}.txt");
            var logfile = await StorageFile.GetFileFromPathAsync(logFilePath);

            return RandomAccessStreamReference.CreateFromFile((IStorageFile)logfile);
        }
    }
}
