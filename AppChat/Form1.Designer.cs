namespace AppChat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ServerIPtxtBox = new TextBox();
            ClientIPtxtBox = new TextBox();
            ServerPortTxtBox = new TextBox();
            ClientPortTxtBox = new TextBox();
            BtnStart = new Button();
            BtnConnect = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            ChatTxtBox = new TextBox();
            MessageTxtBox = new TextBox();
            BtnSend = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 31);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(318, 31);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 1;
            label2.Text = "PORT";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(318, 25);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 3;
            label3.Text = "PORT";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 25);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 2;
            label4.Text = "IP";
            // 
            // ServerIPtxtBox
            // 
            ServerIPtxtBox.Location = new Point(30, 31);
            ServerIPtxtBox.Name = "ServerIPtxtBox";
            ServerIPtxtBox.Size = new Size(221, 23);
            ServerIPtxtBox.TabIndex = 4;
            // 
            // ClientIPtxtBox
            // 
            ClientIPtxtBox.Location = new Point(30, 22);
            ClientIPtxtBox.Name = "ClientIPtxtBox";
            ClientIPtxtBox.Size = new Size(221, 23);
            ClientIPtxtBox.TabIndex = 5;
            // 
            // ServerPortTxtBox
            // 
            ServerPortTxtBox.Location = new Point(359, 28);
            ServerPortTxtBox.Name = "ServerPortTxtBox";
            ServerPortTxtBox.Size = new Size(221, 23);
            ServerPortTxtBox.TabIndex = 6;
            // 
            // ClientPortTxtBox
            // 
            ClientPortTxtBox.Location = new Point(359, 22);
            ClientPortTxtBox.Name = "ClientPortTxtBox";
            ClientPortTxtBox.Size = new Size(221, 23);
            ClientPortTxtBox.TabIndex = 7;
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(615, 26);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(105, 27);
            BtnStart.TabIndex = 10;
            BtnStart.Text = "Start";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // BtnConnect
            // 
            BtnConnect.Location = new Point(615, 22);
            BtnConnect.Name = "BtnConnect";
            BtnConnect.Size = new Size(105, 27);
            BtnConnect.TabIndex = 11;
            BtnConnect.Text = "Connect";
            BtnConnect.UseVisualStyleBackColor = true;
            BtnConnect.Click += BtnConnect_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(ServerPortTxtBox);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(BtnStart);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(ServerIPtxtBox);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(749, 74);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Server";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(ClientPortTxtBox);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(BtnConnect);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(ClientIPtxtBox);
            groupBox2.Location = new Point(12, 92);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(749, 68);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Client";
            // 
            // ChatTxtBox
            // 
            ChatTxtBox.Location = new Point(12, 184);
            ChatTxtBox.Multiline = true;
            ChatTxtBox.Name = "ChatTxtBox";
            ChatTxtBox.Size = new Size(749, 182);
            ChatTxtBox.TabIndex = 14;
            // 
            // MessageTxtBox
            // 
            MessageTxtBox.Location = new Point(12, 386);
            MessageTxtBox.Multiline = true;
            MessageTxtBox.Name = "MessageTxtBox";
            MessageTxtBox.Size = new Size(611, 56);
            MessageTxtBox.TabIndex = 15;
            // 
            // BtnSend
            // 
            BtnSend.Location = new Point(645, 386);
            BtnSend.Name = "BtnSend";
            BtnSend.Size = new Size(125, 56);
            BtnSend.TabIndex = 11;
            BtnSend.Text = "Send";
            BtnSend.UseVisualStyleBackColor = true;
            BtnSend.Click += BtnSend_Click;
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            // 
            // backgroundWorker2
            // 
            backgroundWorker2.DoWork += backgroundWorker2_DoWork;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 478);
            Controls.Add(BtnSend);
            Controls.Add(MessageTxtBox);
            Controls.Add(ChatTxtBox);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox ServerIPtxtBox;
        private TextBox ClientIPtxtBox;
        private TextBox ServerPortTxtBox;
        private TextBox ClientPortTxtBox;
        private Button BtnStart;
        private Button BtnConnect;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private TextBox ChatTxtBox;
        private TextBox MessageTxtBox;
        private Button BtnSend;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
    }
}
