using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Reflection;

using TaskDialogInterop;
using wyUpdate;
//using AutoUpdaterDotNET;

using AndroidCtrl;
using AndroidCtrl.ADB;
using AndroidCtrl.Fastboot;
using AndroidCtrl.Tools;



namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            autoUpdater1.MenuItem = mnuCheckForUpdates;
        }

        //Path of current directory
        public static string KIT = System.AppDomain.CurrentDomain.BaseDirectory;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker UpdateChecker = new BackgroundWorker();
            UpdateChecker.WorkerReportsProgress = true;
            UpdateChecker.DoWork += workerDriver_DoWork;

            UpdateChecker.RunWorkerAsync();

            BackgroundWorker workerDriver = new BackgroundWorker();
            workerDriver.WorkerReportsProgress = true;
            workerDriver.DoWork += workerDriver_DoWork;

            workerDriver.RunWorkerAsync();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //empty
        }

        private int _count = 0;

        void UpdateChecker_DoWork(object sender, DoWorkEventArgs e)
        {
            //AutoUpdater.Start("http://squabbi.square7.ch/toolkit/htconem8/htc-one-m8.xml");
        }

        void workerDriver_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
                {
                    if (Directory.Exists(@"C:\Program Files (x86)\HTC\HTC Driver\Driver Files"))
                    {
                        cBHTCDriver.IsChecked = true;
                    }
                    else
                    {
                        if (Directory.Exists(@"C:\Program Files\HTC\HTC Driver\Driver Files"))
                        {
                            cBHTCDriver.IsChecked = true;
                        }
                    }

                    if (Directory.Exists(@"C:\Program Files (x86)\ClockworkMod\Universal Adb Driver"))
                    {
                        cBUniADB.IsChecked = true;
                    }
                    else
                    {
                        if (Directory.Exists(@"C:\Program Files (x86)\ClockworkMod\Universal Adb Driver"))
                        {
                            cBUniADB.IsChecked = true;
                        }
                    }
                }));

            BackgroundWorker workerDeploy = new BackgroundWorker();
            workerDeploy.WorkerReportsProgress = true;
            workerDeploy.DoWork += workerDeploy_DoWork;

            workerDeploy.RunWorkerAsync();
        }

        void workerDeploy_DoWork(object sender, DoWorkEventArgs e)
        {
            if (File.Exists("./adb/adb.exe"))
            {

            }
            else
            {
                _count++;
            }
            if (File.Exists("./adb/fastboot.exe"))
            {

            }
            else
            {
                _count++;

            }
            if (File.Exists("./adb/aapt.exe"))
            {

            }
            else
            {
                _count++;

            }
            if (File.Exists("./adb/AdbWinApi.dll"))
            {

            }
            else
            {
                _count++;

            }
            if (File.Exists("./adb/AdbWinUsbApi.dll"))
            {

            }
            else
            {
                _count++;

            }

            if (_count > 0)
            {
                MessageBoxResult result = MessageBox.Show("The ADB and Fastboot files have not been deployed correctly. \nWould you like to deploy now? \nNot deploying will make this kit useless.", "Deployment Issue", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {

                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        Start start = new Start();
                        start.ShowDialog();
                    }));
                }
                else
                {
                    // Cancel code here
                    MessageBox.Show("You must deploy the ADB and Fastboot utilities, otherwise the application will NOT work!", "Not Operable", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                ADB.Start();

                // Here we initiate the BASE Fastboot instance
                Fastboot.Instance();

                //This will starte a thread which checks every 10 sec for connected devices and call the given callback
                if (Fastboot.DeviceConnectionMonitor(true))
                {
                    //Here we define our callback function which will be raised if a device connects or disconnects
                    Fastboot.SetDeviceConnectionMonitorCallback = (CallbackDeviceConnectionMonitor)CheckDeviceState;
                }

                // Here we check if ADB is running and initiate the BASE ADB instance (IsStarted() will everytime check if the BASE ADB class exists, if not it will create it)
                if (ADB.IsStarted())
                {
                    //Here we check for connected devices
                    SetDeviceList();

                    //This will starte a thread which checks every 10 sec for connected devices and call the given callback
                    if (ADB.DeviceConnectionMonitor(true))
                    {
                        //Here we define our callback function which will be raised if a device connects or disconnects
                        ADB.SetDeviceConnectionMonitorCallback = (CallbackDeviceConnectionMonitor)CheckDeviceState;
                    }
                }
            }
        }

        public void CheckDeviceState(List<DataModelDevicesItem> devices)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                // Here we refresh our combobox
                SetDeviceList();
            });
        }

        private void btnUniDrivers_Click(object sender, RoutedEventArgs e)
        {
            btnUniDrivers.IsEnabled = false;
            btnHTCDriver.IsEnabled = false;

            if (Directory.Exists("./Data"))
            {
                if (File.Exists("./Data/ADB.msi"))
                {

                    MessageBoxResult result = MessageBox.Show("The universal ADB Drivers have already been downloaded! Would you like to install it? \nPressing no will download the driver again.", "Driver Downloaded Already", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(KIT + "./Data/ADB.msi");
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedADB);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedADB);
                        webClient.DownloadFileAsync(new Uri("http://download.clockworkmod.com/test/UniversalAdbDriverSetup6.msi"), "./Data/ADB.msi");
                    }
                    else
                    {
                        // Cancel code here
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                }
                else
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedADB);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedADB);
                    webClient.DownloadFileAsync(new Uri("http://download.clockworkmod.com/test/UniversalAdbDriverSetup6.msi"), "./Data/ADB.msi");
                }
            }
            else
            {
                Directory.CreateDirectory("./Data/");

                if (File.Exists("./Data/ADB.msi"))
                {

                    MessageBoxResult result = MessageBox.Show("The universal ADB Drivers have already been downloaded! Would you like to install it? \nPressing no will download the driver again.", "Driver Downloaded Already", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(KIT + "./Data/ADB.msi");
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedADB);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedADB);
                        webClient.DownloadFileAsync(new Uri("http://download.clockworkmod.com/test/UniversalAdbDriverSetup6.msi"), "./Data/ADB.msi");
                    }
                    else
                    {
                        // Cancel code here
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                }
                else
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedADB);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedADB);
                    webClient.DownloadFileAsync(new Uri("http://download.clockworkmod.com/test/UniversalAdbDriverSetup6.msi"), "./Data/ADB.msi");
                }
            }
        }

        private void ProgressChangedADB(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void CompletedADB(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            btnUniDrivers.IsEnabled = true;
            btnHTCDriver.IsEnabled = true;

            if (MessageBox.Show("The universal ADB Drivers have been sucessfully downloaded. Would you like to install it now?", "Install driver now?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("./Data/ADB.msi");
            }
            else
            {
                MessageBox.Show("The driver will be stored in the 'Data' folder. Click the button to run it again.", "Driver download Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SetDeviceList()
        {
            string active = String.Empty;
            if (deviceselector.Items.Count != 0)
            {
                active = ((DataModelDevicesItem)deviceselector.SelectedItem).Serial;
            }

            App.Current.Dispatcher.Invoke((Action)delegate
            {
                // Here we refresh our combobox
                deviceselector.Items.Clear();
            });

            // This will get the currently connected ADB devices
            List<DataModelDevicesItem> adbDevices = ADB.Devices();

            // This will get the currently connected Fastboot devices
            List<DataModelDevicesItem> fastbootDevices = Fastboot.Devices();

            foreach (DataModelDevicesItem device in adbDevices)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    // here goes the add command ;)
                    deviceselector.Items.Add(device);
                });
            }
            foreach (DataModelDevicesItem device in fastbootDevices)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    deviceselector.Items.Add(device);
                });
            }
            if (deviceselector.Items.Count != 0)
            {
                int i = 0;
                bool empty = true;
                foreach (DataModelDevicesItem device in deviceselector.Items)
                {
                    if (device.Serial == active)
                    {
                        empty = false;
                        deviceselector.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                if (empty)
                {

                    // This calls will select the BASE class if we have no connected devices
                    ADB.SelectDevice();
                    Fastboot.SelectDevice();


                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        deviceselector.SelectedIndex = 0;
                    });


                }
            }
        }

        private void btnStarted_Click(object sender, RoutedEventArgs e)
        {
            Start start = new Start();
            start.ShowDialog();
        }

        private void btnCredits_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 credits = new AboutBox1();
            credits.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            ADB.Stop();
            ADB.Dispose();
            Fastboot.Dispose();
        }

        private void SelectDeviceInstance(object sender, SelectionChangedEventArgs e)
        {
            if (deviceselector.Items.Count != 0)
            {
                DataModelDevicesItem device = (DataModelDevicesItem)deviceselector.SelectedItem;

                // This will select the given device in the Fastboot and ADB class
                Fastboot.SelectDevice(device.Serial);
                ADB.SelectDevice(device.Serial);
            }
        }

        private void btnADBGo_Click(object sender, RoutedEventArgs e)
        {
            if (cBAdbCmds.Text == "Reboot")
            {
                ADB.Instance().Reboot(IDBoot.REBOOT);
            }
            else if (cBAdbCmds.Text == "Reboot Bootloader")
            {
                ADB.Instance().Reboot(IDBoot.BOOTLOADER);
            }
            else if (cBAdbCmds.Text == "Reboot Recovery")
            {
                ADB.Instance().Reboot(IDBoot.RECOVERY);
            }
            else if (cBAdbCmds.Text == "Pull")
            {
                MessageBox.Show("This is yet to be implemnted. Please stay tuned.", "Under Construction", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else if (cBAdbCmds.Text == "Push")
            {
                if (tBAdbPath.Text == "Select a file...")
                {
                    if (MessageBox.Show("You have not selected a file to complete the command. \nWould you like to locate one now?", "Missing File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        // Create OpenFileDialog
                        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                        // Set filter for file extension and default file extension
                        dlg.DefaultExt = "";
                        dlg.Filter = "All Files (*.*)|*.*";
                        dlg.Title = "Locate Files";

                        // Display OpenFileDialog by calling ShowDialog method
                        Nullable<bool> result = dlg.ShowDialog();

                        // Get the selected file name and display in a TextBox
                        if (result == true)
                        {
                            // Open document
                            string filename = dlg.FileName;
                            tBAdbPath.Text = filename;
                        }
                    }
                    else if (tbAdbDest.Text == "Use a backslash for device path")
                    {
                        MessageBox.Show("You cannot continue unless you select a file.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("You cannot continue unless you select a file.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else if (cBAdbCmds.Text == "Install")
            {
                if (tBAdbPath.Text == "Select a file...")
                {
                    if (MessageBox.Show("You have not selected a file to complete the command. \nWould you like to locate one now?", "Missing File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        // Create OpenFileDialog
                        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                        // Set filter for file extension and default file extension
                        dlg.DefaultExt = "";
                        dlg.Filter = "All Files (*.*)|*.*";
                        dlg.Title = "Locate Files";

                        // Display OpenFileDialog by calling ShowDialog method
                        Nullable<bool> result = dlg.ShowDialog();

                        // Get the selected file name and display in a TextBox
                        if (result == true)
                        {
                            // Open document
                            string filename = dlg.FileName;
                            tBAdbPath.Text = filename;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot continue unless you select a file.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("You have not selected a command.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHTCDriver_Click(object sender, RoutedEventArgs e)
        {
            btnHTCDriver.IsEnabled = false;
            btnUniDrivers.IsEnabled = false;

            if (Directory.Exists("./Data"))
            {
                if (File.Exists("./Data/HTC.msi"))
                {

                    MessageBoxResult result = MessageBox.Show("The HTC Drivers have already been downloaded! Would you like to install it? \nPressing no will download the driver again.", "Driver Downloaded Already", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(KIT + "./Data/HTC.msi");
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedHTC);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedHTC);
                        webClient.DownloadFileAsync(new Uri("http://squabbi.square7.ch/toolkit/htcdriver.msi"), "./Data/HTC.msi");
                    }
                    else
                    {
                        // Cancel code here
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                }
                else
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedHTC);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedHTC);
                    webClient.DownloadFileAsync(new Uri("http://squabbi.square7.ch/toolkit/htcdriver.msi"), "./Data/HTC.msi");
                }
            }
            else
            {
                Directory.CreateDirectory("./Data/");

                if (File.Exists("./Data/HTC.msi"))
                {

                    MessageBoxResult result = MessageBox.Show("The HTC Drivers have already been downloaded! Would you like to install it? \nPressing no will download the driver again.", "Driver Downloaded Already", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(KIT + "./Data/HTC.msi");
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        WebClient webClient = new WebClient();
                        webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedHTC);
                        webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedHTC);
                        webClient.DownloadFileAsync(new Uri("http://squabbi.square7.ch/toolkit/htcdriver.msi"), "./Data/HTC.msi");
                    }
                    else
                    {
                        // Cancel code here
                        btnUniDrivers.IsEnabled = true;
                        btnHTCDriver.IsEnabled = true;
                    }
                }
                else
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedHTC);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChangedHTC);
                    webClient.DownloadFileAsync(new Uri("http://squabbi.square7.ch/toolkit/htcdriver.msi"), "./Data/HTC.msi");
                }
            }
        }

        private void ProgressChangedHTC(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void CompletedHTC(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;
            btnUniDrivers.IsEnabled = true;
            btnHTCDriver.IsEnabled = true;

            if (MessageBox.Show("The HTC Drivers have been sucessfully downloaded. Would you like to install it now?", "Install driver now?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                System.Diagnostics.Process.Start("./Data/HTC.msi");
            }
            else
            {
                MessageBox.Show("The driver will be stored in the 'Data' folder. Click the button to run it again.", "Driver download Completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnFbtGo_Click(object sender, RoutedEventArgs e)
        {
            if (cBFbtCmds.Text == "Reboot")
            {
                Fastboot.Instance().Reboot(IDBoot.REBOOT);
            }
            else if (cBFbtCmds.Text == "Reboot Bootloader")
            {
                Fastboot.Instance().Reboot(IDBoot.BOOTLOADER);
            }
            else if (cBFbtCmds.Text == "RebootRUU")
            {
                Fastboot.Instance().OEM.RebootRUU();
            }
            else if (cBFbtCmds.Text == "Read CID")
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(Fastboot.Instance().OEM.ReadCid());
                ciddiag.Show();
            }
            else if (cBFbtCmds.Text == "Show Identifier Token")
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(Fastboot.Instance().OEM.GetIdentifierToken());
                ciddiag.Show();
            }
            else if (cBFbtCmds.Text == "Relock Bootloader")
            {
                if (MessageBox.Show("Are you sure you want to relock the bootloader?", "Are you sure you want to relock?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(Fastboot.Instance().OEM.Lock());
                    ciddiag.Show();
                }
                else
                {
                    //No
                }
            }
            else if (cBFbtCmds.Text == "S-On Bootloader")
            {
                if (MessageBox.Show("Are you sure you want to S-On?", "Are you sure you want to S-On?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(Fastboot.Instance().OEM.WriteSecureFlag("3"));
                    ciddiag.Show();
                }
                else
                {
                    //No
                }
            }
            else
            {
                MessageBox.Show("You have not selected a command.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnXDA_Click(object sender, RoutedEventArgs e)
        {
            TaskDialogOptions config = new TaskDialogOptions();

            config.Owner = this;
            config.Title = "Useful Links";
            config.MainInstruction = "Here are some links that you may want to see";
            config.Content = "I have here some links that may help you in a time of need or "
                           + "just for general information on things about the HTC One M8.";
            config.CommandButtons = new string[] {
            "Toolkit Thread", "FUU Guide\nFirmware Flashing\nCID Numbers\nError Handling", "Nevermind" };
            config.MainIcon = VistaTaskDialogIcon.Information;

            TaskDialogResult res = TaskDialog.Show(config);
            if (res.CommandButtonResult == 0)
            {
                System.Diagnostics.Process.Start("http://forum.xda-developers.com/showthread.php?t=2694925");
            }
            else if (res.CommandButtonResult == 1)
            {
                System.Diagnostics.Process.Start("http://forum.xda-developers.com/htc-one-m8/development/progress-fuu-m8-t2813792");
            }
            else if (res.CommandButtonResult == 3)
            {
            }
        }

        private void btnHtcDev_Click(object sender, RoutedEventArgs e)
        {
            if (deviceselector.Items.Count == 0)
            {
                MessageBox.Show("A device has not been detected as of yet. Wait 10 seconds or diagnose the issue.", "No Device", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            IDDeviceState state = General.CheckDeviceState(ADB.Instance().DeviceID);
            if (state == IDDeviceState.DEVICE)
            {
                ADB.Instance().Reboot(IDBoot.BOOTLOADER);
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(Fastboot.Instance().OEM.GetIdentifierToken());
                ciddiag.Show();
                MessageBox.Show("You must continue from the HTC Dev website.", "Redirecting to HTCDev.com", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Diagnostics.Process.Start("http://www.htcdev.com/bootloader/");
            }
            else if (state == IDDeviceState.FASTBOOT)
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(Fastboot.Instance().OEM.GetIdentifierToken());
                ciddiag.Show();
                MessageBox.Show("You must continue from the HTC Dev website.", "Redirecting to HTCDev.com", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Diagnostics.Process.Start("http://www.htcdev.com/bootloader/");
            }
        }

        private void btnLocateAdb_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = "";
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.Title = "Locate Files";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tBAdbPath.Text = filename;
            }
        }

        private void btnSOff_Click(object sender, RoutedEventArgs e)
        {
            S_OffUtils soff = new S_OffUtils();
            soff.ShowDialog();
        }

        private void btnFbtFlash_Click(object sender, RoutedEventArgs e)
        {
            if (tBFbtPath.Text == "")
            {
                if (MessageBox.Show("You have not selected a file to complete the command. \nWould you like to locate one now?", "Missing File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Create OpenFileDialog
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = "";
                    dlg.Filter = "All Files (*.*)|*.*";
                    dlg.Title = "Locate Files";

                    // Display OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = dlg.ShowDialog();

                    // Get the selected file name and display in a TextBox
                    if (result == true)
                    {
                        // Open document
                        string filename = dlg.FileName;
                        tBFbtPath.Text = filename;
                    }
                }
                else
                {
                    MessageBox.Show("You cannot continue unless you select a file.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (cBFbtPart.Text == "")
            {
                MessageBox.Show("You cannot continue unless you select a partition.", "Partition not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (cBFbtPart.Text == "Boot")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.BOOT, tBFbtPath.Text));
                }
                else if (cBFbtPart.Text == "Cache")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.CACHE, tBFbtPath.Text));
                }
                else if (cBFbtPart.Text == "Data")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.DATA, tBFbtPath.Text));
                }
                else if (cBFbtPart.Text == "Hboot")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.HBOOT, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Kernel")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.KERNEL, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Misc")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.MISC, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Radio")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.RADIO, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Ramdisk")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.RAMDISK, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Recovery")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.RECOVERY, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "System")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.SYSTEM, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Unlock Token")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.UNLOCKTOKEN, tBFbtPath.Text));

                }
                else if (cBFbtPart.Text == "Zip")
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().Flash(IDDevicePartition.ZIP, tBFbtPath.Text));

                }
            }
        }

        private void btnLocateFbtFlsh_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = "";
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.Title = "Locate Files";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tBFbtPath.Text = filename;
            }
        }

        private void btnErase_Click(object sender, RoutedEventArgs e)
        {
            if (deviceselector.Text == "")
            {
                MessageBox.Show("A device has not been detected! Please wait 10 seconds or diagnose the issue!", "No device detected", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (cBFbtPart.Text == "")
                {
                    MessageBox.Show("You cannot continue unless you select a partition.", "Partition not Selected", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (cBFbtPart.Text == "Boot")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.BOOT));
    
                    }
                    else if (cBFbtPart.Text == "Cache")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.CACHE));
    
                    }
                    else if (cBFbtPart.Text == "Data")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.DATA));
    
                    }
                    else if (cBFbtPart.Text == "Hboot")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.HBOOT));
    
                    }
                    else if (cBFbtPart.Text == "Kernel")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.KERNEL));
    
                    }
                    else if (cBFbtPart.Text == "Misc")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.MISC));
    
                    }
                    else if (cBFbtPart.Text == "Radio")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.RADIO));
    
                    }
                    else if (cBFbtPart.Text == "Ramdisk")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.RAMDISK));
                    }
                    else if (cBFbtPart.Text == "Recovery")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.RECOVERY));
                    }
                    else if (cBFbtPart.Text == "System")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.SYSTEM));
                    }
                    else if (cBFbtPart.Text == "Unlock Token")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.UNLOCKTOKEN));
                    }
                    else if (cBFbtPart.Text == "Zip")
                    {
                        CidDialog ciddiag = CidDialog.Instance;
                        ciddiag.Show();
                        ciddiag.Add(Fastboot.Instance().Erase(IDDevicePartition.ZIP));
                    }
                }
            }
        }

        private void btnAdbShell_Click(object sender, RoutedEventArgs e)
        {
            CidDialog ciddiag = CidDialog.Instance;
            ciddiag.Show();
            ciddiag.Add(ADB.Instance().ShellCmd(tbAdbShell.Text));
        }

        private void btnORS_Click(object sender, RoutedEventArgs e)
        {
            ORS ors = new ORS();
            ors.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CidDialog ciddiag = CidDialog.Instance;
            ciddiag.Show();
        }

        private void btnSelectPathAdb_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog mediabackup = new System.Windows.Forms.FolderBrowserDialog();
            mediabackup.SelectedPath = "C:\\";
            mediabackup.ShowNewFolderButton = true;
            System.Windows.Forms.DialogResult result = mediabackup.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if (Directory.Exists(mediabackup.SelectedPath))
                {
                    tbAdbDest.Text = mediabackup.SelectedPath;
                    //Slected
                }
                else
                {
                    //Not Selected
                }
            }
            mediabackup.Dispose();
        }

        private void btnAdbFlagCmds_Click(object sender, RoutedEventArgs e)
        {
            ScottyAdbCmds scotty = new ScottyAdbCmds();
            scotty.Show();
        }

        private void btnRecoveries_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks to @Captian_Throwback !", "", MessageBoxButton.OK, MessageBoxImage.Information);
            System.Diagnostics.Process.Start("http://themikmik.com/showthread.php?t=16278");
        }

        private void btnBoot_Click(object sender, RoutedEventArgs e)
        {
            if (tBFbtPath.Text == "")
            {
                if (MessageBox.Show("You have not selected a file. Would you like to select one now?", "Missing File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Create OpenFileDialog
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = "";
                    dlg.Filter = "All Files (*.*)|*.*";
                    dlg.Title = "Locate Files";

                    // Display OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = dlg.ShowDialog();

                    // Get the selected file name and display in a TextBox
                    if (result == true)
                    {
                        // Open document
                        string filename = dlg.FileName;
                        tBFbtPath.Text = filename;
                    }
                }
                else
                {
                    //nothing
                }
            }
            else
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Show();
                ciddiag.Add(Fastboot.Instance().Boot(tBFbtPath.Text,"",10));
            }
        }

        private void btnRoot_Click(object sender, RoutedEventArgs e)
        {
            Root root = new Root();
            root.Show();
        }

        private void btnExec_Click(object sender, RoutedEventArgs e)
        {
            if (cBFbtOem.Text == "")
            {
                MessageBox.Show("You must select a command to execute!");
            }
            else if (cBFbtOem.Text == "RebootRUU")
            {
                Fastboot.Instance().OEM.RebootRUU();
            }
            else if (cBFbtOem.Text == "Lock Bootloader")
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure?";
                config.MainInstruction = "You are about to RELOCK the bootloader.";
                config.Content = "You will need to go through an unlocking procedure again"
                               + "to unlock the bootloader. (Either through HTCDev or the ADB Bootloader Commands)";
                config.CommandButtons = new string[] {
            "No", "No", "Yes" };
                config.MainIcon = VistaTaskDialogIcon.Information;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CommandButtonResult == 0)
                {
                }
                else if (res.CommandButtonResult == 1)
                {
                }
                else if (res.CommandButtonResult == 2   )
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(Fastboot.Instance().OEM.Lock());
                }
            }
        }
    }
}