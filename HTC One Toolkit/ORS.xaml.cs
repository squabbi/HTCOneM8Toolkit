using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using TaskDialogInterop;

using AndroidCtrl;
using AndroidCtrl.ADB;

namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for ORS.xaml
    /// </summary>
    public partial class ORS : Window
    {
        public ORS()
        {
            InitializeComponent();
        }

        private void btnORSGo_Click(object sender, RoutedEventArgs e)
        {
            if (tbFlashPath.Text == "No File Selected")
            {
                TaskDialogOptions diag = new TaskDialogOptions();

                diag.Owner = this;
                diag.Title = "No File Found";
                diag.MainInstruction = "File not found.";
                diag.Content = "A ZIP file was not located to be flashed.";
                diag.CommandButtons = new string[] {
                    "I would like to &Browse for a ZIP file now","No thanks" };
                diag.MainIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(diag);

                if (res.CommandButtonResult == 0)
                {
                    // Create OpenFileDialog
                    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                    // Set filter for file extension and default file extension
                    dlg.DefaultExt = "";
                    dlg.Filter = "ZIP Files (*.zip)|*.zip";
                    dlg.Title = "Locate Files";

                    // Display OpenFileDialog by calling ShowDialog method
                    Nullable<bool> result = dlg.ShowDialog();

                    // Get the selected file name and display in a TextBox
                    if (result == true)
                    {
                        // Open document
                        string filename = dlg.FileName;
                        tbFlashPath.Text = filename;
                    }
                }

                //MessageBox.Show("No zip file has been selected. Please select one and try again.");
            }
            else
            {
                if (cbWipeCache.IsChecked == true)
                {
                    ADB.Instance().Device.OpenRecoveryScript.WipeCache = true;
                }
                else if (cbWipeData.IsChecked == true)
                {
                    ADB.Instance().Device.OpenRecoveryScript.WipeData = true;
                }
                else if (cbWipeDalvik.IsChecked == true)
                {
                    ADB.Instance().Device.OpenRecoveryScript.WipeDalvik = true;
                }

                ADB.Instance().Device.OpenRecoveryScript.InstallFilePath = tbFlashPath.Text;
                ADB.Instance().Device.OpenRecoveryScript.WriteScriptToDevice();
                


            }
        }

        private void btnBrowseFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = "";
            dlg.Filter = "ZIP Files (*.zip)|*.zip";
            dlg.Title = "Locate Files";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tbFlashPath.Text = filename;
            }
        }

        private void btnNandroidGo_Click(object sender, RoutedEventArgs e)
        {
            if (cbBackupBoot.IsChecked == true)
            {
                ADB.Instance().Device.OpenRecoveryScript.BackupBoot = true;
               
            }
            else
            {
                ADB.Instance().Device.OpenRecoveryScript.BackupBoot = false;
            }
            
            ADB.Instance().Device.OpenRecoveryScript.WriteScriptToDevice();

            ADB.Instance().Reboot(IDBoot.RECOVERY);
            
        }
    }
}
