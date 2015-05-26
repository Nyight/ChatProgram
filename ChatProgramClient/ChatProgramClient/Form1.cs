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
    public partial class Form1 : Form
    {
        //TcpClient _client;

        Server _chatServer;

        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;

            _chatServer = new Server("127.0.0.1", 3000);
            _chatServer.Connect();
            //ConnectToServer();

            //var msg = MessageType.HELLO;
            //SendMessage(msg);
        }

        //private void ConnectToServer()
        //{
        //    _client = new TcpClient();

        //    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

        //    _client.Connect(serverEndPoint);
        //}

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SendMessage(Message.goodbye.ToString());
            _chatServer.Disconnect();
        }

        //private void SendMessage(String m)
        //{

        //    NetworkStream clientStream = _client.GetStream();

        //    ASCIIEncoding encoder = new ASCIIEncoding();
        //    byte[] buffer = encoder.GetBytes(m);

        //    clientStream.Write(buffer, 0, buffer.Length);
        //    clientStream.Flush();
        //}
    }
}
