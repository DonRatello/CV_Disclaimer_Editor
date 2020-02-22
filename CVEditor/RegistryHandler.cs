using Microsoft.Win32;

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace CVEditor
{
    public class RegistryHandler
    {
        public static string ReadKey(string valueName)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey($"Software\\{Constants.AppName}", true);
                return key.GetValue(valueName).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        public static void WriteKey(string valueName, string value)
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey($"Software\\{Constants.AppName}", true);
                key.SetValue(valueName, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void CheckMainSettings()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

                var mainKey = key.OpenSubKey(Constants.AppName, true);
                if (mainKey == null)
                {
                    key.CreateSubKey(Constants.AppName);
                    mainKey = key.OpenSubKey(Constants.AppName, true);
                }

                mainKey.SetValue("AppName", Assembly.GetExecutingAssembly().GetName().Name);
                mainKey.SetValue("AppVersion", fvi.FileVersion);
                mainKey.SetValue("StartTime", DateTime.Now.ToString());
                mainKey.SetValue("Architecture", Environment.Is64BitOperatingSystem ? "x64" : "x86");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
