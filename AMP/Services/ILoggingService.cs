using Serilog;
using System;
using System.Diagnostics;
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
        bool EmitToConsole { get; set; }
        void Debug(string message);
        void Information(string message);
     
        void Warning(string message);
        void Error(string message);
        void Fatal(string message);
     
        Task ClearAsync();
    }

    public class LocalFileLogger : ILogging
    {
       
        static bool _isEnabled = true;
#if DEBUG
        static bool _emitToConsole = true;
#else
        static bool _emitToConsole = false;
#endif

        public bool IsEnabled { get { return _isEnabled; } set { _isEnabled = value; } }
        public bool EmitToConsole { get { return _emitToConsole; } set { _emitToConsole = value; } }

        public LocalFileLogger()
        {
            
        }

        public void Information(string message)
        {
            if (IsEnabled)
            {
                LogUtil.Information(message);
                if (EmitToConsole)
                    System.Diagnostics.Debug.WriteLine($"{message}");
            }

          
        }
        public void Debug(string message)
        {
            if (IsEnabled)
                LogUtil.Debug(message);
            if (EmitToConsole)
                System.Diagnostics.Debug.WriteLine($"{message}");

        }

        public void Warning(string message)
        {
            if (IsEnabled)
                LogUtil.Warning(message);
            if (EmitToConsole)
                System.Diagnostics.Debug.WriteLine($"{message}");
        }

        public void Error(string message)
        {
            if (IsEnabled)
                LogUtil.Error(message);
            if (EmitToConsole)
                System.Diagnostics.Debug.WriteLine($"{message}");
        }

        public void Fatal(string message)
        {
            
            if (IsEnabled)
                LogUtil.Error(message);
            if (EmitToConsole)
                System.Diagnostics.Debug.WriteLine($"{message}");
        }

     

        public async Task ClearAsync()
        {
            await LogUtil.ClearAsync();
        }
    }

    public static class LogUtil
    {
        static string LOG_BASE_NAME = "log";

        static LogUtil()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, $"{LOG_BASE_NAME}.txt");
#if DEBUG
            Log.Logger = new LoggerConfiguration().MinimumLevel.Verbose().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day).CreateLogger();
#else
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day).CreateLogger();
#endif

        }

        public static void Debug(string message)
        {
           Log.Logger.Debug(message);
        }

        public static void Information(string message)
        {
            Log.Logger.Information(message);


        }
        public static void Warning(string message)
        {
            Log.Logger.Warning(message);
        }
        public static void Error(string message)
        {
            Log.Logger.Error(message);
        }

        public static void Fatal(string message)
        {
            Log.Logger.Fatal(message);
        }

        public static async Task<RandomAccessStreamReference> GetFileStreamReference()
        {
            var logFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, $"{LOG_BASE_NAME}{DateTime.Today.ToString("yyyyMMdd")}.txt");
            var logfile = await StorageFile.GetFileFromPathAsync(logFilePath);

            return RandomAccessStreamReference.CreateFromFile((IStorageFile)logfile);
        }

        public static async Task ClearAsync()
        {
            var todaysLog = $"{LOG_BASE_NAME}{DateTime.Today.ToString("yyyyMMdd")}.txt";

            var files = await ApplicationData.Current.LocalFolder.GetFilesAsync();

            foreach (var file in files)
            {
                if (file.Name != todaysLog && file.Name.EndsWith(".txt"))
                    await file.DeleteAsync();
            }
        }
    }
}
