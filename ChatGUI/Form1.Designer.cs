namespace ChatApp
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.TextBox textBoxPeerAddress;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBoxMessage = new TextBox();
            textBoxChat = new TextBox();
            textBoxPeerAddress = new TextBox();
            buttonSend = new Button();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // textBoxMessage
            // 
            textBoxMessage.Location = new Point(67, 316);
            textBoxMessage.Margin = new Padding(4, 3, 4, 3);
            textBoxMessage.Name = "textBoxMessage";
            textBoxMessage.Size = new Size(536, 23);
            textBoxMessage.TabIndex = 0;
            // 
            // textBoxChat
            // 
            textBoxChat.Location = new Point(67, 12);
            textBoxChat.Margin = new Padding(4, 3, 4, 3);
            textBoxChat.Multiline = true;
            textBoxChat.Name = "textBoxChat";
            textBoxChat.ReadOnly = true;
            textBoxChat.ScrollBars = ScrollBars.Vertical;
            textBoxChat.Size = new Size(536, 297);
            textBoxChat.TabIndex = 1;
            // 
            // textBoxPeerAddress
            // 
            textBoxPeerAddress.Location = new Point(67, 346);
            textBoxPeerAddress.Margin = new Padding(4, 3, 4, 3);
            textBoxPeerAddress.Name = "textBoxPeerAddress";
            textBoxPeerAddress.Size = new Size(536, 23);
            textBoxPeerAddress.TabIndex = 2;
            // 
            // buttonSend
            // 
            buttonSend.Location = new Point(611, 314);
            buttonSend.Margin = new Padding(4, 3, 4, 3);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(88, 27);
            buttonSend.TabIndex = 3;
            buttonSend.Text = "Send";
            buttonSend.UseVisualStyleBackColor = true;
            buttonSend.Click += buttonSend_Click;
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(611, 344);
            buttonConnect.Margin = new Padding(4, 3, 4, 3);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(88, 27);
            buttonConnect.TabIndex = 4;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(611, 378);
            buttonDisconnect.Margin = new Padding(4, 3, 4, 3);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(88, 27);
            buttonDisconnect.TabIndex = 5;
            buttonDisconnect.Text = "Disconnect";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(67, 372);
            label1.Name = "label1";
            label1.Size = new Size(238, 45);
            label1.TabIndex = 6;
            label1.Text = "Use 'IP:Port' to connect to an other instance\r\nIP = 127.0.01\r\n\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 320);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 7;
            label2.Text = "Msg:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 350);
            label4.Name = "label4";
            label4.Size = new Size(58, 15);
            label4.TabIndex = 9;
            label4.Text = "User port:";
            // 
            // ChatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(735, 451);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(buttonSend);
            Controls.Add(textBoxPeerAddress);
            Controls.Add(textBoxChat);
            Controls.Add(textBoxMessage);
            Margin = new Padding(4, 3, 4, 3);
            Name = "ChatForm";
            Text = "Chat Application";
            Load += ChatForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label1;
        private Label label2;
        private Label label4;
    }
}
