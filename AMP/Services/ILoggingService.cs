using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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

        static LocalFileLogger()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "log.txt");


            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day).CreateLogger();
            //System.Diagnostics.Debug.WriteLine($"{DateTime.Today.ToString("yyyyMMdd")}");


        }



        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }

        public void Information(string message)
        {
            if (IsEnabled)
                Log.Information(message);
        }
        public void Debug(string message)
        {
            if (IsEnabled)
                Log.Debug(message);
        }

        public void Warning(string message)
        {
            if (IsEnabled)
                Log.Warning(message);
        }

        public void Error(string message)
        {
            if (IsEnabled)
                Log.Error(message);
        }
    }

    public static class LogUtil
    {
        private static LocalFileLogger _logger;

        static LogUtil()
        {
            _logger = new LocalFileLogger();
        }

        public static void Information(string message)
        {

            _logger.Information(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);


        }
        public static void Warning(string message)
        {
            _logger.Warning(message);
        }
        public static void Error(string message)
        {
            _logger.Error(message);
        }
    }
}
