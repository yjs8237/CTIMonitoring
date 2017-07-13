using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CTIMonitoringTEST
{
    public partial class Form1 : Form
    {

        UseDll useDll;

        public Form1()
        {
            InitializeComponent();
            useDll = new UseDll(this);
            CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ipA = textBox1.Text;
            string ipB = textBox2.Text;
            int port = Convert.ToInt16(textBox3.Text);
            useDll.monConnect(ipA, ipB, port);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            useDll.monDisConnect();
        }

        public void setData(string data)
        {
            textBox4.AppendText(data);
        }
    }
}
