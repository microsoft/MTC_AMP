using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AMP.Services
{
    public interface ISettingsService
    {
        T GetSetting<T>(string key, T defaultValue = default(T));
        void SaveSetting<T>(string key, T value);

        bool HasSetting(string key);
    }

    public class LocalSettingsService : ISettingsService
    {

        public T GetSetting<T>(string key, T defaultValue = default(T))
        {
            var setting = ApplicationData.Current.LocalSettings.Values[key];

            if (setting is null)
                return defaultValue;

            return (T)setting;
        }

        public void SaveSetting<T>(string key, T value)
        {
            ApplicationData.Current.LocalSettings.Values[key] = value;
        }

        public bool HasSetting(string key)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(key);
        }

    }
}
