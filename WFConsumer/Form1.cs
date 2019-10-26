using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebClientConsummer.Models;

namespace WFConsumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {

            var rpcClient = new RpcClient();

            Console.WriteLine(" [x] Requesting ...");
            var response = rpcClient.Call(textBox1.Text);
            textBox2.Text += textBox1.Text + "   --->   " + response + System.Environment.NewLine;
            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();

        }
    }
}
