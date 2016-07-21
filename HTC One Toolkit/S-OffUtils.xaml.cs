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

using AndroidCtrl;
using AndroidCtrl.ADB;
using AndroidCtrl.Fastboot;
using AndroidCtrl.Tools;

namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for S_OffUtils.xaml
    /// </summary>
    public partial class S_OffUtils : Window
    {
        public S_OffUtils()
        {
            InitializeComponent();
        }

        public static string KIT = System.AppDomain.CurrentDomain.BaseDirectory;

        private void btnLocateFw_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = "firewater";
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.Title = "Locate Firewater";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tBFwPath.Text = filename;
            }
        }

        private void btnReboot_Click(object sender, RoutedEventArgs e)
        {
            ADB.Instance().Reboot(IDBoot.REBOOT);
        }

        private void btnPushFw_Click(object sender, RoutedEventArgs e)
        {
            if (tBFwPath.Text == "")
            {
                if (MessageBox.Show("You have not selected firewater to complete the command. \nWould you like to locate it now?", "Missing File", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Create OpenFileDialog
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = "firewater";
                    dlg.Filter = "All Files (*.*)|*.*";
                    dlg.Title = "Locate Firewater";

                    // Display OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = dlg.ShowDialog();

                    // Get the selected file name and display in a TextBox
                    if (result == true)
                    {
                        // Open document
                        string filename = dlg.FileName;
                        tBFwPath.Text = filename;
                    }
                }
                else
                {
                    MessageBox.Show("You cannot continue unless you select a file.", "Missing File", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(ADB.Instance().Push(tBFwPath.Text, "/data/local/tmp/firewater"));
                ciddiag.Show();
            }
        }

        private void btnFwSetPerms_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This requires ROOT access. Check your device for an SU prompt after clicking OK.", "ROOT Required Next", MessageBoxButton.OK, MessageBoxImage.Information);
            ADB.Instance().ShellCmd("su -c 'chmod 755 /data/local/tmp/firewater'");
        }

        private void btnLaunFw_Click(object sender, RoutedEventArgs e)
        {
            var process = System.Diagnostics.Process.Start("CMD.exe", KIT + "/adb/adb shell /data/local/tmp/firewater");
            process.WaitForExit(); 
        }

        private void btnFirewater_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://firewater-soff.com/firewaterdownload-3");
        }
    }
}
