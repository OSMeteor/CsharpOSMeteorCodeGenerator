using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CsharpOSMeteorCodeGenerator
{
    public partial class about : Form
    {
        public about()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(this.linkLabel1.Text);
            System.Diagnostics.Process.Start(startInfo);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //http://wpa.qq.com/msgrd?v=3&uin=406662428&site=qq&menu=yes
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = this.linkLabel2.Text;
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(url);
            System.Diagnostics.Process.Start(startInfo);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string url = @"http://wpa.qq.com/msgrd?v=3&uin=406662428&site=qq&menu=yes";
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(url);
            System.Diagnostics.Process.Start(startInfo);
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = @"http://weibo.com/systembreakdown";
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(url);
            System.Diagnostics.Process.Start(startInfo);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string url = @"http://shang.qq.com/wpa/qunwpa?idkey=ac8d7a501d7ae846eb4c47d87128097fdedd6c2db54b00c49c7b850875c9352a";
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo(url);
            System.Diagnostics.Process.Start(startInfo);
        }
    }
}
