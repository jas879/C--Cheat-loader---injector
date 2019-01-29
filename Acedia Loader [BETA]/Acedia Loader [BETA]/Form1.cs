using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Components;
using System.Management;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Acedia_Loader__BETA_
{

    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }       

        private string GetHWID()
        {
            string str = "";
            try
            {
                string str2 = Environment.GetFolderPath(Environment.SpecialFolder.System).Substring(0, 1);
                ManagementObject obj1 = new ManagementObject("win32_logicaldisk.deviceid=\"" + str2 + ":\"");
                obj1.Get();
                str = obj1["VolumeSerialNumber"].ToString();
            }
            catch (Exception)
            {
            }
            return str; 
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetHWID());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/theacedia");
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            string hw = GetHWID();

            string str;
            using (StreamReader strr = new StreamReader(HttpWebRequest.Create("your server" + hw).GetResponse().GetResponseStream()))
            str = strr.ReadToEnd();

                if (str == "{\"status\":\"ok\"}")
                {
                    MessageBox.Show("Welcome!");

                    this.Hide();
                    Form2 f2 = new Form2();
                    f2.ShowDialog();
                    this.Close();
                }

                else
                {
                    MessageBox.Show("Please buy a license");
                    Thread.Sleep(100);
                    System.Diagnostics.Process.Start("https://vk.com/theacedia?w=product-175497991_2881207%2Fquery");
                }

            
        }

    }
}

/* Project by Aiko https://vk.com/aikosimidzu*/
