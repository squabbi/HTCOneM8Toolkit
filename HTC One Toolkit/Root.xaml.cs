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
using mshtml;

using AndroidCtrl.ADB;

namespace HTC_One_Toolkit
{
    /// <summary>
    /// Interaction logic for Root.xaml
    /// </summary>
    public partial class Root : Window
    {
        public Root()
        {
            InitializeComponent();
        }

        private void btnShowSite_Click(object sender, RoutedEventArgs e)
        {
            if (cbCheck1.IsChecked == false)
            {
                this.Width = Convert.ToDouble("750");
                cbCheck1.IsChecked = true;
            }
            else
            {
                this.Width = Convert.ToDouble("279");
                cbCheck1.IsChecked = false;
            }
        }

        private void wbSU_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var html = wbSU.Document as mshtml.HTMLDocument;
            html.parentWindow.scroll(0, 100000000);
        }

        private void btnPushSU_Click(object sender, RoutedEventArgs e)
        {
            CidDialog ciddiag = CidDialog.Instance;
            ciddiag.Add(ADB.Instance().Push("./Data/SuperSU.zip","/sdcard/"));
            ciddiag.Show();
        }

        private void btnRbtRec_Click(object sender, RoutedEventArgs e)
        {
            ADB.Instance().Reboot(AndroidCtrl.IDBoot.RECOVERY);
        }
    }
}
