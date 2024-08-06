using System;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApp
{
    public partial class ChatForm : Form
    {
        private Chat chat;
        public ChatForm()
        {
            InitializeComponent();
            chat = new Chat(GetAvailablePort(), UpdateChat);
        }

        private void UpdateChat(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateChat), message);
            }
            else
            {
                textBoxChat.AppendText(message + Environment.NewLine);
            }
        }

        private async void buttonSend_Click(object sender, EventArgs e)
        {
            await chat.BroadcastMessageAsync(textBoxMessage.Text);
            textBoxMessage.Clear();
        }

        private async void buttonConnect_Click(object sender, EventArgs e)
        {
            await chat.ConnectToPeerAsync(textBoxPeerAddress.Text);
        }

        private async void buttonDisconnect_Click(object sender, EventArgs e)
        {
            await chat.DisconnectPeerAsync(textBoxPeerAddress.Text);
        }

        private async void ChatForm_Load(object sender, EventArgs e)
        {
            await chat.StartAsync();
        }
        private int GetAvailablePort()
        {
            return 1000 + new Random().Next(1, 1000);
        }
    }
}

