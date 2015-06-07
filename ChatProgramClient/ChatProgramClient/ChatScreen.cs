using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatProgramClient
{
    public partial class ChatScreen : Form
    {
        Server _chatServer;

        public ChatScreen()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
            btnSendMsg.Click += btnSendMsg_Click;
            txtbxMessage.TextChanged += txtbxMessage_TextChanged;

            _chatServer = new Server("127.0.0.1", 3000);
            //_chatServer.Connect();
        }

        void txtbxMessage_TextChanged(object sender, EventArgs e)
        {
            btnSendMsg.Enabled = false;
            if (txtbxMessage.TextLength > 0)
                btnSendMsg.Enabled = true;
        }

        void btnSendMsg_Click(object sender, EventArgs e)
        {
            
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //_chatServer.Disconnect();
        }
    }
}
