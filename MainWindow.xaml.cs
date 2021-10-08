using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XCleaner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Task> Tasks = new List<Task>();

        public static BackgroundWorker bw = new BackgroundWorker();

        public static bool temp;
        public static bool browsers;
        public static bool CachePr;
        public static bool SysReports;
        public static bool cleaner_RAM;
        public static bool En_taskMgr;
        public static bool En_regedit;
        public static bool removeOneDrive;
        public static bool disableUAC;
        public static bool disableWIndowsDifender;
        public static bool disableSmartscreen;
        public static bool disableHistoryFiles;
        public static bool disableXbox;
        public static bool cleaning_regedit;
        public static bool cleaning_dinamic_registry;
        public static bool cleaning_system_dumps;
        public static bool disable_hide_monitoring_system;
        public static bool disable_Windows_sync;
        public static bool disable_Windows_Events;
        public static bool Disabling_location;
        public int ctop = 10;

        public MainWindow()
        {
            InitializeComponent();
            bw.DoWork += Start;
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!bw.IsBusy)
            {
                if (checkBox1.IsChecked == true)
                {
                    temp = true;
                }
                if (checkBox2.IsChecked == true)
                {
                    browsers = true;
                }
                if (checkBox3.IsChecked == true)
                {
                    CachePr = true;
                }
                if (CheckBox4.IsChecked == true)
                {
                    SysReports = true;
                }


                if(CheckBox5.IsChecked == true)
                {
                    En_taskMgr = true;
                }
                if (CheckBox6.IsChecked == true)
                {
                    En_regedit = true;
                }
                if (CheckBox7.IsChecked == true)
                {
                    removeOneDrive = true;
                }
                if (CheckBox8.IsChecked == true)
                {
                    disableUAC = true;
                }
                if (CheckBox9.IsChecked == true)
                {
                    disableWIndowsDifender = true;
                }
                if (CheckBox10.IsChecked == true)
                {
                    disableSmartscreen = true;
                }
                if(CheckBox11.IsChecked == true)
                {
                    disableHistoryFiles = true;
                }
                if(CheckBox12.IsChecked== true)
                {
                    disable_hide_monitoring_system = true;
                }
                if(CheckBox13.IsChecked == true)
                {
                    disableXbox = true;
                }
                if(CheckBox14.IsChecked== true)
                {
                    cleaning_regedit = true;
                }
                if(CheckBox15.IsChecked == true)
                {
                    cleaning_dinamic_registry = true;
                }
                if(CheckBox16.IsChecked == true)
                {
                    cleaning_system_dumps = true;
                }
                if(CheckBox17.IsChecked == true)
                {
                    disable_Windows_sync = true;
                }
                if(CheckBox18.IsChecked == true)
                {
                    disable_Windows_Events = true;
                }
                if(CheckBox19.IsChecked == true)
                {
                    Disabling_location = true;
                }
                if(CheckBox20.IsChecked == true)
                {
                    cleaner_RAM = true;
                }
                checkBox1.IsEnabled = false;
                checkBox2.IsEnabled = false;
                checkBox3.IsEnabled = false;
                CheckBox4.IsEnabled = false;
                CheckBox5.IsEnabled = false;
                CheckBox6.IsEnabled = false;
                CheckBox7.IsEnabled = false;
                CheckBox8.IsEnabled = false;
                CheckBox9.IsEnabled = false;
                CheckBox10.IsEnabled = false;
                CheckBox11.IsEnabled = false;
                CheckBox12.IsEnabled = false;
                CheckBox13.IsEnabled = false;
                CheckBox14.IsEnabled = false;
                CheckBox15.IsEnabled = false;
                CheckBox16.IsEnabled = false;
                CheckBox17.IsEnabled = false;
                CheckBox18.IsEnabled = false;
                CheckBox19.IsEnabled = false;
                button1.IsEnabled = false;
                bw.RunWorkerAsync();
                
            }
            else
            {
                MessageBox.Show("Чистка выполняется");
            }
            
        }

        private async void Start(object sender, DoWorkEventArgs e)
        {
            await Task.Run(() =>
            {
            if (temp == true)
            {
                Tasks.Add(Cleaning.temp());
            }
            if (browsers == true)
            {
                Tasks.Add(Cleaning.Browsers());
            }
            if (CachePr == true)
            {
                Tasks.Add(Cleaning.CachePrograms());
            }
            if (SysReports == true)
            {
                Tasks.Add(Cleaning.SystemReports());
            }


            if (En_taskMgr == true)
            {
                Tasks.Add(disableTelemetry.EnableTaskMgr());
            }
            if (En_regedit == true)
            {
                Tasks.Add(disableTelemetry.EnableRegedit());
            }
            if (removeOneDrive == true)
            {
                Tasks.Add(disableTelemetry.RemoveOneDrive());
            }
            if (disableUAC == true)
            {
                Tasks.Add(disableTelemetry.DisableUAC());
            }
            if (disableWIndowsDifender == true)
            {
                Tasks.Add(disableTelemetry.DisableWindowsDifender());
            }
            if (disableSmartscreen == true)
            {
                Tasks.Add(disableTelemetry.DisableSmartScreen());
            }
            if (disableHistoryFiles == true)
            {
                Tasks.Add(disableTelemetry.disable_History_Files());
            }
            if (disableXbox == true)
            {
                Tasks.Add(disableTelemetry.disable_XBox());
            }
            if(cleaning_regedit == true)
                {
                    Tasks.Add(Cleaning.Clear_Regedit());
                }
            if(cleaning_dinamic_registry == true)
                {
                    Tasks.Add(Cleaning.cleaning_dinamic_registry());
                }
            if(cleaning_system_dumps == true)
                {
                    Tasks.Add(Cleaning.systemDumps());
                }
            if(disable_hide_monitoring_system == true)
                {
                    Tasks.Add(disableTelemetry.disable_hide_monitoring_system());
                }
            if(disable_Windows_sync == true)
                {
                    Tasks.Add(disableTelemetry.disable_Windows_sync());
                }
            if(Disabling_location == true)
                {
                    Tasks.Add(disableTelemetry.Disabling_location());
                }
            if(cleaner_RAM == true)
                {
                    Tasks.Add(Cleaning.cleaner_RAM());
                }

            Task.WaitAll(Tasks.ToArray());
            

            Dispatcher.Invoke((Action)(() =>
                    {
                        checkBox1.IsEnabled = true;
                        checkBox2.IsEnabled = true;
                        checkBox3.IsEnabled = true;
                        CheckBox4.IsEnabled = true;
                        CheckBox5.IsEnabled = true;
                        CheckBox6.IsEnabled = true;
                        CheckBox7.IsEnabled = true;
                        CheckBox8.IsEnabled = true;
                        CheckBox9.IsEnabled = true;
                        CheckBox10.IsEnabled = true;
                        CheckBox11.IsEnabled = true;
                        CheckBox12.IsEnabled = true;
                        CheckBox13.IsEnabled = true;
                        CheckBox14.IsEnabled = true;
                        CheckBox15.IsEnabled = true;
                        CheckBox16.IsEnabled = true;
                        CheckBox17.IsEnabled = true;
                        CheckBox18.IsEnabled = true;
                        CheckBox19.IsEnabled = true;
                        button1.IsEnabled = true;
                        
                        Label1.Content = "system reboot required";
                        
                    }));
                
                MessageBox.Show($"Computer has been cleaned" +
                $" {Cleaning.File_size} kb" + Environment.NewLine +
                $"deleted {Cleaning.ALL_files} files", "XCleaner", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if(button2.Content.ToString() == "eng")
            {
                button2.Content = "ru";
                button1.Content = "применить";
                checkBox1.Content = "очистить temp";
                checkBox2.Content = "очистить браузеры";
                checkBox3.Content = "очистить кеш програм";
                CheckBox4.Content = "очистить системные репорты";

                CheckBox5.Content = "разблокировать диспетчер задач";
                CheckBox6.Content = "разблокировать рестр";
                CheckBox7.Content = "полностью удалить OneDrive";
                CheckBox8.Content = "отключить UAC";
                CheckBox9.Content = "отключить защитник windows";
                CheckBox10.Content = "отключить SmartScreen";
                CheckBox12.Content = "отключение телеметрии windows";
                CheckBox14.Content = "чистить рестр";
                CheckBox13.Content = "отключение Xbox";

            }
            else
            {
                button2.Content = "eng";
                button1.Content = "Apply";

                checkBox1.Content = "clean temp folder";
                checkBox2.Content = "clean browsers";
                checkBox3.Content = "clean cache programs";
                CheckBox4.Content = "clean system reports";

                CheckBox5.Content = "Enable taskmgr";
                CheckBox6.Content = "Enable regedit";
                CheckBox7.Content = "remove OneDrive";
                CheckBox8.Content = "disable UAC";
                CheckBox9.Content = "disable Windows Defender";
                CheckBox10.Content = "disable SmartScreen";
                CheckBox14.Content = "cleaning regedit";
                CheckBox12.Content = "disable windows telemetry";
                CheckBox13.Content = "disable Xbox";
            }
        }   
    }
}
