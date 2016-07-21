using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace HTC_One_Toolkit
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
        }

        String theVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            lblVer.Text = theVersion;
        }
    }
}
