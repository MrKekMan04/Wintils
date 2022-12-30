using Microsoft.Win32;

namespace Wintils.Helpers
{
    public static class RegistryHelper
    {
        public static object GetCurrentUserRegistryValue(string key) => GetCurrentUserRegistryValue(key, null);

        public static object GetCurrentUserRegistryValue(string key, object defaultValue)
        {
            const string subKey = "Software\\Wintils\\config";

            CreateSubKeyIfNotExists(subKey);

            return Registry.CurrentUser.OpenSubKey("Software\\Wintils\\config")?.GetValue(key, defaultValue);
        }

        public static void SetCurrentUserRegistryValue(string key, object value)
        {
            const string subKey = "Software\\Wintils\\config";

            CreateSubKeyIfNotExists(subKey);

            Registry.CurrentUser.OpenSubKey(subKey, true)?.SetValue(key, value);
        }

        private static void CreateSubKeyIfNotExists(string subKey)
        {
            if (Registry.CurrentUser.OpenSubKey(subKey) == null)
                Registry.CurrentUser.CreateSubKey(subKey, true);
        }

        public static bool SetAutorunValue(bool autorun)
        {
            var name = System.Reflection.Assembly.GetEntryAssembly()?.FullName;
            var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var registryKey =
                Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);

            try
            {
                if (autorun)
                    registryKey.SetValue(name, exePath);
                else if (name != null)
                    registryKey.DeleteValue(name);

                registryKey.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}