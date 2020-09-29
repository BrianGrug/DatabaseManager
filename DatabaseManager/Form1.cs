using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;

namespace DatabaseManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServiceController serviceController = new ServiceController("MongoDB");
            Process[] processes = Process.GetProcessesByName("redis-server");
            if(processes.Length > 0)
            {
                button1.Text = "Stop Redis";
            }
            else
            {
                button1.Text = "Start Redis";
            }
            if(serviceController.Status == ServiceControllerStatus.Running)
            {
                button2.Text = "Stop Mongo";
            }
            else
            {
                button2.Text = "Start Mongo";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("redis-server");
            if (processes.Length > 0)
            {
                processes[0].Kill();
                button1.Text = "Start Redis";
            }
            else
            {
                Process process1 = new Process();

                process1.StartInfo = new ProcessStartInfo(textBox1.Text);
                process1.Start();
                button1.Text = "Stop Redis";
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceController serviceController = new ServiceController("MongoDB");

            if (serviceController.Status == ServiceControllerStatus.Running)
            {
                serviceController.Stop();
                button2.Text = "Start Mongo";
                return;
            }
            else
            {
                serviceController.Start();
                button2.Text = "Stop Mongo";
            }
        }
    }
}