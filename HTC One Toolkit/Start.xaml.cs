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
using System.IO;

using AndroidCtrl;
using AndroidCtrl.ADB;
using AndroidCtrl.Fastboot;
using AndroidCtrl.Tools;

namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        public Start()
        {
            InitializeComponent();
        }

        //Path of current directory
        public static string KIT = System.AppDomain.CurrentDomain.BaseDirectory;

        private int _count = 0;

        private void btnDeploy_Click(object sender, RoutedEventArgs e)
        {
            Deploy.ADB();
            _count = 0;
            if (cBVerify.IsChecked == true)
            {
                if (File.Exists("./adb/adb.exe"))
                {
                    lbladb.Content = "Checked!";
                    lbladb.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladb.Content = "Not Found!";
                }
                if (File.Exists("./adb/fastboot.exe"))
                {
                    lblfastboot.Content = "Checked!";
                    lblfastboot.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lblfastboot.Content = "Not Found!";
                }
                if (File.Exists("./adb/aapt.exe"))
                {
                    lblaapt.Content = "Checked!";
                    lblaapt.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lblaapt.Content = "Not Found!";
                }
                if (File.Exists("./adb/AdbWinApi.dll"))
                {
                    lbladbwinapi.Content = "Checked!";
                    lbladbwinapi.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladbwinapi.Content = "Not Found!";
                }
                if (File.Exists("./adb/AdbWinUsbApi.dll"))
                {
                    lbladbwinusbapi.Content = "Checked!";
                    lbladbwinusbapi.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladbwinusbapi.Content = "Not Found!";
                }

                if (_count > 0)
                {
                    lblFailFiles.Content = _count + " files failed to validate.";
                    lblFailFiles.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    lblFailFiles.Content = "All files accounted for!";
                    lblFailFiles.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(KIT + "./adb"))
            {
                lblDeployStat.Text = ("Folder Present, Check below.");
                lblDeployStat.Foreground = new SolidColorBrush(Colors.Orange);

                if (cBVerify.IsChecked == true)
                {
                    if (File.Exists("./adb/adb.exe"))
                    {
                        lbladb.Content = "Checked!";
                        lbladb.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        _count++;
                        lbladb.Content = "Not Found!";
                    }
                    if (File.Exists("./adb/fastboot.exe"))
                    {
                        lblfastboot.Content = "Checked!";
                        lblfastboot.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        _count++;
                        lblfastboot.Content = "Not Found!";
                    }
                    if (File.Exists("./adb/aapt.exe"))
                    {
                        lblaapt.Content = "Checked!";
                        lblaapt.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        _count++;
                        lblaapt.Content = "Not Found!";
                    }
                    if (File.Exists("./adb/AdbWinApi.dll"))
                    {
                        lbladbwinapi.Content = "Checked!";
                        lbladbwinapi.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        _count++;
                        lbladbwinapi.Content = "Not Found!";
                    }
                    if (File.Exists("./adb/AdbWinUsbApi.dll"))
                    {
                        lbladbwinusbapi.Content = "Checked!";
                        lbladbwinusbapi.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        _count++;
                        lbladbwinusbapi.Content = "Not Found!";
                    }

                    if (_count > 0)
                    {
                        lblFailFiles.Content = _count + " files failed to validate.";
                        lblFailFiles.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        lblFailFiles.Content = "All files accounted for!";
                        lblFailFiles.Foreground = new SolidColorBrush(Colors.Green);
                    }
                }
            }
            else
            {
                lblDeployStat.Text = ("Not Deployed!");
                lblDeployStat.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void btnStatus_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("There were " + _count + " unvalidated files.");
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            _count = 0;

                if (File.Exists("./adb/adb.exe"))
                {
                    lbladb.Content = "Checked!";
                    lbladb.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladb.Content = "Not Found!";
                    lbladb.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (File.Exists("./adb/fastboot.exe"))
                {
                    lblfastboot.Content = "Checked!";
                    lblfastboot.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lblfastboot.Content = "Not Found!";
                    lblfastboot.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (File.Exists("./adb/aapt.exe"))
                {
                    lblaapt.Content = "Checked!";
                    lblaapt.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lblaapt.Content = "Not Found!";
                    lblaapt.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (File.Exists("./adb/AdbWinApi.dll"))
                {
                    lbladbwinapi.Content = "Checked!";
                    lbladbwinapi.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladbwinapi.Content = "Not Found!";
                    lbladbwinapi.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (File.Exists("./adb/AdbWinUsbApi.dll"))
                {
                    lbladbwinusbapi.Content = "Checked!";
                    lbladbwinusbapi.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    _count++;
                    lbladbwinusbapi.Content = "Not Found!";
                    lbladbwinusbapi.Foreground = new SolidColorBrush(Colors.Red);
                }

                if (_count > 0)
                {
                    lblFailFiles.Content = _count + " files failed to validate.";
                    lblFailFiles.Foreground = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    lblFailFiles.Content = "All files accounted for!";
                    lblFailFiles.Foreground = new SolidColorBrush(Colors.Green);
                }
            }
        }
    }
