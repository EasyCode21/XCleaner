using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace XCleaner
{
    public class Cleaning
    {
        public static List<string> ExtensionBase = new List<string>();
        public static string dled;


        public static List<string> DirBase = new List<string>();
        public static int File_size;
        public static int ALL_files = 0;


        public static List<string> regeditPath_base = new List<string>();
        public static async Task temp()
        {
            await Task.Run(() =>
            {
                StreamReader ext = new StreamReader("rash.txt"); // cчитывание расширений
                while (!ext.EndOfStream)
                {
                    ExtensionBase.Add(ext.ReadLine());
                }
                string Temp = Path.GetTempPath();
                RecursiveDel.EnumerateFolder_DeliteRecur(Temp);
                RecursiveDel.EnumerateFolder_DeliteRecur(@"C:\Windows\Temp");
                StreamReader ex2t = new StreamReader("DirBase.txt");
                while (!ex2t.EndOfStream)
                {
                    DirBase.Add(ex2t.ReadLine());
                }
                try
                {
                    foreach (string dir in DirBase)
                    {
                        RecursiveDel.EnumerateFolder_DeliteRecur(dir);
                    }
                }
                catch { }

                HelpMetods.EnumerateFilesDeliteRecur(@"C:\", ExtensionBase); // очистка всего диска по расширениям
            });
        }
        [DllImport("KERNEL32.DLL", EntryPoint =
         "SetProcessWorkingSetSize", SetLastError = true,
         CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize32Bit
         (IntPtr pProcess, int dwMinimumWorkingSetSize,
         int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint =
           "SetProcessWorkingSetSize", SetLastError = true,
           CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize64Bit
           (IntPtr pProcess, long dwMinimumWorkingSetSize,
           long dwMaximumWorkingSetSize);

        public static void FlushMem()
        {
            GC.Collect();

            GC.WaitForPendingFinalizers();

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {

                SetProcessWorkingSetSize32Bit(System.Diagnostics
                   .Process.GetCurrentProcess().Handle, -1, -1);

            }
        }

        public static async Task cleaner_RAM()
        {
            await Task.Run(() =>
            {
                FlushMem();
            });
        }
        public static async Task Clear_Regedit()
        {
            await Task.Run(() =>
            {
                StreamReader ext = new StreamReader("regeditPath_base.txt");
                while (!ext.EndOfStream)
                {
                    regeditPath_base.Add(ext.ReadLine());
                }
                foreach (string Key in regeditPath_base)
                {
                    try
                    {
                        if (Key.IndexOf(@"\HKEY_CURRENT_USER\") == 0)
                        {
                            string good_str = Key.Replace(@"\HKEY_CURRENT_USER\", "");

                            Registry.CurrentUser.DeleteSubKeyTree(good_str);
                        }
                        if (Key.IndexOf(@"\HKEY_CLASSES_ROOT\") == 0)
                        {
                            string good_str = Key.Replace(@"\HKEY_CLASSES_ROOT\", "");

                            Registry.ClassesRoot.DeleteSubKeyTree(good_str);
                        }
                        if (Key.IndexOf(@"\HKEY_LOCAL_MACHINE\") == 0)
                        {
                            string good_str = Key.Replace(@"\HKEY_LOCAL_MACHINE\", "");

                            Registry.LocalMachine.DeleteSubKeyTree(good_str);
                        }
                        if (Key.IndexOf(@"\HKEY_USERS\") == 0)
                        {
                            string good_str = Key.Replace(@"\HKEY_USERS\", "");

                            Registry.Users.DeleteSubKeyTree(good_str);
                        }
                        if (Key.IndexOf(@"\HKEY_CURRENT_CONFIG\") == 0)
                        {
                            string good_str = Key.Replace(@"\HKEY_CURRENT_CONFIG\", "");

                            Registry.CurrentConfig.DeleteSubKeyTree(good_str);
                        }
                    }
                    catch { }
                }

            });
        }
        //FuncRecusr(TreeNode a)
        //{
        //    foreach (TreeNode CholdNOde in a.Nodes)
        //    {
        //        a = ChildNode;
        //        FuncRecurs(a);
        //    }
        //}



        static RegFindValue RegFind(RegistryKey key, string find)
        {
            if (key == null || string.IsNullOrEmpty(find))
                return null;

            string[] props = key.GetValueNames();
            object value = null;

            if (props.Length != 0)
                foreach (string property in props)
                {
                    if (property.IndexOf(find, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        return new RegFindValue(key, property, null, RegFindIn.Property);
                    }

                    value = key.GetValue(property, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                    if (value is string && ((string)value).IndexOf(find, StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        return new RegFindValue(key, property, (string)value, RegFindIn.Value);
                    }
                }

            string[] subkeys = key.GetSubKeyNames();
            RegFindValue retVal = null;

            if (subkeys.Length != 0)
            {
                foreach (string subkey in subkeys)
                {
                    try
                    {
                        retVal = RegFind(key.OpenSubKey(subkey, RegistryKeyPermissionCheck.ReadSubTree), find);
                    }
                    catch (Exception ex)
                    {
                        // err msg, if need
                    }
                    if (retVal != null)
                    {
                        return retVal;
                    }
                }
            }
            key.Close();
            return null;
        }

        class RegFindValue
        {
            RegistryKey regKey;
            string mProps;
            string mVal;
            RegFindIn mWhereFound;

            public RegistryKey Key
            { get { return regKey; } }

            public string Property
            { get { return mProps; } }

            public string Value
            { get { return mVal; } }

            public RegFindIn WhereFound
            { get { return mWhereFound; } }

            public RegFindValue(RegistryKey key, string props, string val, RegFindIn where)
            {
                regKey = key;
                mProps = props;
                mVal = val;
                mWhereFound = where;
            }
        }

        enum RegFindIn
        {
            Property,
            Value
        }
        public static async Task cleaning_dinamic_registry()
        {
            await Task.Run(() =>
            {
                string temp = Path.GetTempPath();

                RegFind(Registry.ClassesRoot, "temp");


                //string temp2_test = "temp";
                //RegistryKey regKey = Registry.LocalMachine;
                //string[] pth = regKey.GetValueNames();
                //foreach (string path in pth)
                //{
                //    if(path.IndexOf(temp2_test) == 0)
                //    {
                //        MessageBox.Show(path);
                //    }
                //}
            });
        }
        public static async Task systemDumps()
        {
            await Task.Run(() =>
            {
                RecursiveDel.EnumerateFolder_DeliteRecur(Environment.GetEnvironmentVariable("WINDIR", EnvironmentVariableTarget.Machine)+ "\\Minidump");
               
            });
        }
        public static async Task SystemReports()
        {
            await Task.Run(() =>
            {
                string AppDataPathLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string System32Folder = Environment.GetFolderPath(Environment.SpecialFolder.System);
                string OSDrive = System32Folder.Substring(0, 3);
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\ProgramData\\Microsoft\\Windows Security Health\\Logs");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Internet Explorer\\CacheStorage");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Windows\\PowerShell");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\Windows\\Panther\\UnattendGC");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\Windows\\ServiceProfiles\\NetworkService\\AppData\\Local\\Microsoft\\Windows\\DeliveryOptimization\\Logs");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Windows\\WER\\ReportArchive");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Windows\\WER\\ReportQueue");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Windows\\WER\\Temp");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Windows\\WER\\ERC");
                RecursiveDel.EnumerateFolder_DeliteRecur(ProgramData + "\\Microsoft\\Windows\\WER\\ReportArchive");
                RecursiveDel.EnumerateFolder_DeliteRecur(ProgramData + "\\Microsoft\\Windows\\WER\\ReportQueue");
                RecursiveDel.EnumerateFolder_DeliteRecur(ProgramData + "\\Microsoft\\Windows\\WER\\Temp");
                RecursiveDel.EnumerateFolder_DeliteRecur(ProgramData + "\\Microsoft\\Windows\\WER\\ERC");
                RecursiveDel.EnumerateFolder_DeliteRecur(System32Folder +"\\LogFiles");
                RecursiveDel.EnumerateFolder_DeliteRecur(OSDrive + "\\inetpub\\logs\\LogFiles");
            });
        }

        public static async Task CachePrograms()
        {
            await Task.Run(() =>
            {
                string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string AppDataPathLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\JetBrains");

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Visual Studio Setup");


                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Adobe");

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\vstelemetry");

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Sun");

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Nikola_Тesla");

                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Red Gate");
            });
        }

        public static async Task Browsers()
        {
            await Task.Run(() =>
            {
                string AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

                string AppDataPathLocal = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                //Opera
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Yandex");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Opera Software\\Opera GX Stable\\Cache");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Opera Software\\Opera GX Stable\\Code Cache");

                //Firefox
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPath + "\\Mozilla\\Firefox");
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\Program Files (x86)\\Mozilla Maintenance Service\\logs");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Mozilla\\Firefox\\Profiles\\pltrha1r.default-release\\cache2\\entries");
                //Microsoft Edge
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Edge\\User Data\\Default\\Cache");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Edge\\User Data\\Default\\Code Cache\\js");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Edge\\User Data\\Default\\Session Storage");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Microsoft\\Edge\\User Data\\ShaderCache\\GPUCache");
                //Yandex Browser
                RecursiveDel.EnumerateFolder_DeliteRecur("C:\\ProgramData\\Yandex\\YandexBrowser");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\BrowserMetrics");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Crashpad");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Default\\blob_storage");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Default\\Cache");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Default\\Code Cache\\js\\index-dir");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Default\\data_reduction_proxy_leveldb");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\Default\\Wallpapers\\store");
                RecursiveDel.EnumerateFolder_DeliteRecur(AppDataPathLocal + "\\Yandex\\YandexBrowser\\User Data\\gpu_configs_overrides");
            });
        }
    }
}
