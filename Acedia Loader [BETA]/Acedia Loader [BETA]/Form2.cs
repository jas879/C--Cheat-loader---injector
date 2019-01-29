using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework.Components;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Media;
using Acedia_Loader__BETA_.InjectionLibrary;
using JLibrary.PortableExecutable;
using System.Diagnostics;

namespace Acedia_Loader__BETA_
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        string put = @"AcMLd9E4You\";
        string dgm = @"Ld4Rmpr.dll";
        string tm = @"C:\ProgramData\";

        private void Form2_Load(object sender, EventArgs e)
        {
            string pop = tm;
            string path = pop + put;

            Directory.CreateDirectory(path);

            DirectoryInfo di = Directory.CreateDirectory(path);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;       

        }

        string realDLL = String.Empty;
        string[] availableMethods = {"ManualMap"};

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            System.Diagnostics.Process.Start("https://vk.com/theacedia");

        }


        void deleteFolder(string folder)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);

                DirectoryInfo[] diA = di.GetDirectories();

                FileInfo[] fi = di.GetFiles();

                foreach (FileInfo f in fi)
                {
                    f.Delete();
                }

                foreach (DirectoryInfo df in diA)
                {
                    deleteFolder(df.FullName);
                    if (df.GetDirectories().Length == 0 && df.GetFiles().Length == 0) df.Delete();
                }
            }
            catch (Exception)
            {

            }
           

        }


        private void metroButton1_Click(object sender, EventArgs e)
        {

            FileInfo fi2 = new FileInfo(tm + put + dgm);
            string deletePath = tm + put;
            deleteFolder(deletePath);
            
            WebClient wc = new WebClient();
            string url = "link for download file";
            string save_path = tm + put;
            string name = "name for you file";
            wc.DownloadFile(url, save_path + name);
            Thread.Sleep(100);

            string realDLL = tm + put + dgm;


            InjectionMethod injector = null;

            injector = InjectionMethod.Create(InjectionMethodType.ManualMap);


            Process[] processes = Process.GetProcessesByName("csgo");
            if (processes.Length <= 0)
            {
                fi2.Delete();
                Directory.Delete(deletePath);
                MessageBox.Show("Сначала запустите CS:GO", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                int processId = processes[0].Id;
                IntPtr result = IntPtr.Zero;
                using (PortableExecutable pe = new PortableExecutable(realDLL))
                {
                    result = injector.Inject(pe, processId);
                }
                if (result != IntPtr.Zero)
                {
                    playSimpleSound();
                    File.WriteAllText(Properties.res.paste, realDLL + ";");
                    
                    MessageBox.Show("Успешный инжект!");
                    Application.Exit();
                }
                else
                {
                    if (injector.GetLastError() != null)
                    {
                        fi2.Delete();
                        Directory.Delete(deletePath);
                        MessageBox.Show(injector.GetLastError().Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(0);
                    }
                }

            }

            void playSimpleSound()
            {
                SoundPlayer simpleSound = new SoundPlayer(Properties.Resources.beep);
                simpleSound.Play();
                simpleSound.Dispose();
            }

            fi2.Delete();
            deleteFolder(deletePath);
            Directory.Delete(deletePath);
          
        }
    }
}
