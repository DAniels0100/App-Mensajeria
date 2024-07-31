using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace AppChat
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        public StreamReader streamR;
        public StreamWriter streamW;
        public string receive;
        public string txtSend;


        public Form1()
        {
            InitializeComponent();

            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerIPtxtBox.Text = address.ToString();
                }
            }

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, int.Parse(ServerPortTxtBox.Text));
            listener.Start();
            client = listener.AcceptTcpClient();
            streamR = new StreamReader(client.GetStream());
            streamW = new StreamWriter(client.GetStream());
            streamW.AutoFlush = true;
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker2.WorkerSupportsCancellation = true;


        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(ClientIPtxtBox.Text), int.Parse(ClientPortTxtBox.Text));
            client.Connect(iPEnd);

            try
            {
                ChatTxtBox.AppendText("Connected to server\n");
                streamW = new StreamWriter(client.GetStream());
                streamR = new StreamReader(client.GetStream());
                streamW.AutoFlush = true;
                backgroundWorker1.RunWorkerAsync();
                backgroundWorker2.WorkerSupportsCancellation = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    receive = streamR.ReadLine();
                    this.ChatTxtBox.Invoke(new MethodInvoker(delegate ()
                    {
                        ChatTxtBox.AppendText("\nyou:" + receive + "\n");
                    }));
                    receive = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                streamW.WriteLine(txtSend);
                this.ChatTxtBox.Invoke(new MethodInvoker(delegate ()
                {
                    ChatTxtBox.AppendText("\nme:" + txtSend + "\n");
                }));
            }
            else
            {
                MessageBox.Show("sending Failed");
            }

            backgroundWorker2.CancelAsync();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (MessageTxtBox.Text != "")
            {
                txtSend = MessageTxtBox.Text;
                backgroundWorker2.RunWorkerAsync();
            }
            MessageTxtBox.Text = "";
        }
    }
}
