using RabbitMQ.Core.MessageQueue;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RabbitMQ.Producer.TestApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string channel)
        {
            InitializeComponent();
            this.textBox1.Text = channel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sendMsgSta;
            sendMsgSta = RabbitPushServer.Client.SendMsgProxy($"(this.comboBox1.Text)Key_{this.textBox1.Text}",this.textBox2.Text);
            this.richTextBox1.Text += $"{sendMsgSta}{Environment.NewLine}";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.comboBox1.Items.AddRange(new string[] { "weixin", "qq", "weibo", "dingding"});
            this.comboBox1.SelectedIndex = 1;
        }
    }
}
