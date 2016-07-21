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
using System.Diagnostics;

using AndroidCtrl.ADB;
using AndroidCtrl.Fastboot;
using AndroidCtrl.Tools;

using TaskDialogInterop;

namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for ScottyAdbCmds.xaml
    /// </summary>
    public partial class ScottyAdbCmds : Window
    {
        public ScottyAdbCmds()
        {
            InitializeComponent();
        }

        private void btnMidThread_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forum.xda-developers.com/showthread.php?t=2708581");
        }

        private void btnMidGo_Click(object sender, RoutedEventArgs e)
        {
            if (lbMidList.SelectedIndex == 0)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "AT&T, Developer and Unlocked Editions of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B12000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue","&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Show();
                    ciddiag.Add(ADB.Instance().ShellCmd("su '-ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x32\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384'"));
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 1)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "Google Play Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B17000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x37\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384'"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 2)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "T-Mobile Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B13000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x33\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384'"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 3)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "HTC Europe Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B10000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x30\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 4)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "Rogers Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B16000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x36\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 5)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "Wind (Canada) Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B13000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x33\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else if (lbMidList.SelectedIndex == 6)
            {
                TaskDialogOptions config = new TaskDialogOptions();

                config.Owner = this;
                config.Title = "Are you sure about your selection?";
                config.MainInstruction = "You have chosen to change the Model ID of your device";
                config.Content = "The Model ID you have chosen is the one for the " +
                                 "0P6B11000 Edition of the phone. Click 'Show Details' to show the MID number.";
                config.ExpandedInfo = "The Model ID is: 0P6B11000";
                //config.VerificationText = "Don't show me this message again";
                config.CustomButtons = new string[] { "&Continue", "&Cancel" };
                config.MainIcon = VistaTaskDialogIcon.Information;
                //config.FooterText = "Optional footer text with an icon can be included.";
                //config.FooterIcon = VistaTaskDialogIcon.Warning;

                TaskDialogResult res = TaskDialog.Show(config);
                if (res.CustomButtonResult == 0)
                {
                    CidDialog ciddiag = CidDialog.Instance;
                    ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x30\x00\x50\x00\x36\x00\x42\x00\x31\x00\x31\x00\x30\x00\x30\x00\x30' | dd of=/dev/block/mmcblk0p5 bs=1 seek=16384"));
                    ciddiag.Show();
                }
                else
                {
                    //Nothing
                }
            }
            else
            {
                MessageBox.Show("You have not selected anything. You cannot continue.");
            }
        }

        private void btnRmvTampered_Click(object sender, RoutedEventArgs e)
        {
            TaskDialogOptions config = new TaskDialogOptions();

            config.Owner = this;
            config.Title = "Are you sure about your selection?";
            config.MainInstruction = "You have chosen to remove the tampered banner from the bootloader";
            config.Content = "This will remove the tampered banner from the bootloader. " +
                             "";
            config.ExpandedInfo = "The tampered banner is: **TAMPERED**";
            //config.VerificationText = "Don't show me this message again";
            config.CustomButtons = new string[] { "&Continue", "&Cancel" };
            config.MainIcon = VistaTaskDialogIcon.Information;
            //config.FooterText = "Optional footer text with an icon can be included.";
            //config.FooterIcon = VistaTaskDialogIcon.Warning;

            TaskDialogResult res = TaskDialog.Show(config);
            if (res.CustomButtonResult == 0)
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x00' | dd of=/dev/block/mmcblk0p6 bs=1 seek=5314564"));
                ciddiag.Show();
                ADB.Instance().Reboot(AndroidCtrl.IDBoot.BOOTLOADER);
            }
            else
            {
                //Nothing
            }
        }

        private void btnLock_Click(object sender, RoutedEventArgs e)
        {
            TaskDialogOptions config = new TaskDialogOptions();

            config.Owner = this;
            config.Title = "Are you sure about your selection?";
            config.MainInstruction = "You have chosen to set your ";
            config.Content = "This will lock your bootloader." +
                             "Only use this if you are 100% sure you won't screw anything up.";
            config.ExpandedInfo = "The banner is: **LOCKED**";
            //config.VerificationText = "Don't show me this message again";
            config.CustomButtons = new string[] { "&Continue", "&Cancel" };
            config.MainIcon = VistaTaskDialogIcon.Information;
            //config.FooterText = "Optional footer text with an icon can be included.";
            //config.FooterIcon = VistaTaskDialogIcon.Warning;

            TaskDialogResult res = TaskDialog.Show(config);
            if (res.CustomButtonResult == 0)
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne '\x00\x00\x00\x00' | dd of=/dev/block/mmcblk0p2 bs=1 seek=33796"));
                ciddiag.Show();
                ADB.Instance().Reboot(AndroidCtrl.IDBoot.BOOTLOADER);
            }
            else
            {
                //Nothing
            }
        }

        private void btnUnlocked_Click(object sender, RoutedEventArgs e)
        {
            TaskDialogOptions config = new TaskDialogOptions();

            config.Owner = this;
            config.Title = "Are you sure about your selection?";
            config.MainInstruction = "You have chosen to set your ";
            config.Content = "This will unlock your bootloader." +
                             "";
            config.ExpandedInfo = "The banner is: **UNLOCKED**";
            //config.VerificationText = "Don't show me this message again";
            config.CustomButtons = new string[] { "&Continue", "&Cancel" };
            config.MainIcon = VistaTaskDialogIcon.Information;
            //config.FooterText = "Optional footer text with an icon can be included.";
            //config.FooterIcon = VistaTaskDialogIcon.Warning;

            TaskDialogResult res = TaskDialog.Show(config);
            if (res.CustomButtonResult == 0)
            {
                CidDialog ciddiag = CidDialog.Instance;
                ciddiag.Add(ADB.Instance().ShellCmd("su -c 'echo -ne \"HTCU\" | dd of=/dev/block/mmcblk0p2 bs=1 seek=33796'"));
                ciddiag.Show();
                ADB.Instance().Reboot(AndroidCtrl.IDBoot.BOOTLOADER);
            }
            else
            {
                //Nothing
            }
        }
    }
}
