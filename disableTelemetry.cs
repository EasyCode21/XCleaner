using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XCleaner
{
    public class disableTelemetry
    {
        public static async Task EnableTaskMgr()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.CurrentUser;
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true).SetValue("DisableTaskMgr", 0);
                rg.Close();
            });
        }
        public static async Task disable_History_Files()
        {
            await Task.Run(() =>
            {
                HelpMetods.powershell_WaitforExit("sc stop \"fhsvc\"");
            });
        }
        public static async Task disable_XBox()
        {
            await Task.Run(() =>
            {
                HelpMetods.powershell_WaitforExit("sc stop \"xbgm\"");
            });
        }
        public static async Task disable_Windows_Events()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.LocalMachine;
                rg.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", true).SetValue("Start", 4);
                rg.Close();
            });
        }
        public static async Task EnableRegedit()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.CurrentUser;
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true).SetValue("DisableRegistryTools", 0);
                rg.Close();
            });
        }
        public static async Task Disabling_location()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.LocalMachine;
                rg.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).SetValue("DisableLocation", 1);
                rg.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).SetValue("DisableLocationScripting", 1);
                rg.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows\LocationAndSensors", true).SetValue("DisableWindowsLocationProvider", 1);
                rg.Close();
            });
        }
        public static async Task disable_hide_monitoring_system()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.LocalMachine;
                rg.CreateSubKey(@"SYSTEM\CurrentControlSet\Services\CDPUserSvc", true).SetValue("Start", 4);
                rg.Close();
            });
        }
        public static async Task disable_Windows_sync()
        {
            await Task.Run(() =>
            {
                RegistryKey rg = Registry.CurrentUser;
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Accessibility", true).SetValue("Enabled", 0);
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\BrowserSettings", true).SetValue("Enabled", 0);
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Credentials", true).SetValue("Enabled", 0);
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Language", true).SetValue("Enabled", 0);
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Personalization", true).SetValue("Enabled", 0);
                rg.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\SettingSync\Groups\Windows", true).SetValue("Enabled", 0);
                rg.Close();
            });
        }
        public static async Task RemoveOneDrive()
        {
            await Task.Run(() =>
            {
                HelpMetods.killProcc("OneDrive");
                Thread.Sleep(100);
                string Users = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

                string AppDataPathLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\OneDrive");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\ProgramData\\Microsoft OneDrive");
                RecursiveDel.EnumerateFolder_DeliteRecur(Users + "\\OneDrive");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\Windows\\WinSxS\\wow64_microsoft-windows-onedrive-setup_31bf3856ad364e35_10.0.19041.1_none_e585f901f9ce93e6");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\Windows\\WinSxS\\wow64_microsoft-windows-settingsync-onedrive_31bf3856ad364e35_10.0.19041.1_none_8f92ee2b150bb996");
            });
        }
        public static async Task DisableSmartScreen()
        {
            await Task.Run(() =>
            {
                HelpMetods.killProcc("smartscreen");
                RegistryKey rg = Registry.LocalMachine;
                rg.CreateSubKey(@"SOFTWARE\Policies\Microsoft\Windows").SetValue("EnableSmartScreen", 0);
                rg.Close();
                HelpMetods.killProcc("smartscreen");
                try
                {
                    File.Delete(@"C:\Windows\System32\smartscreen.exe");
                } finally
                {
                    File.Delete(@"C:\Windows\System32\smartscreen.exe");
                }
            });
        }
        public static async Task DisableWindowsDifender()
        {
            await Task.Run(() =>
            {
                HelpMetods.killProcc("SecurityHealthSystray");
                HelpMetods.powershell_WaitforExit("sc delete \"WarpJITSvc\"");
                HelpMetods.RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0"); //Windows 10 1903 Redstone 6
                HelpMetods.RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
                HelpMetods.RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
                HelpMetods.RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
                HelpMetods.RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
                HelpMetods.killProcc("SecurityHealthSystray");
            });
        }

        public static async Task DisableUAC()
        {
            await Task.Run(() =>
            {
                RegistryKey Key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
                Key.SetValue("EnableLUA", 0);
                Key.Close();
            });
        }
    }
}
