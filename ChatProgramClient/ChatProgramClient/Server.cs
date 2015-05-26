using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatProgramClient
{
    class Server
    {
        [Flags]
        internal enum Command { Goodbye = 0, Hello = 1, Message = 2, Login = 4};
        [Flags]
        internal enum MessageType { All = 0, Private = 1 };

        private IPEndPoint _endPoint;
        private TcpClient _server;
        private ASCIIEncoding _encoder;

        #region constructors
        public Server(String ip, int port)
            : this(IPAddress.Parse(ip), port)
        {

        }

        public Server(IPAddress ip, int port)
            : this(new IPEndPoint(ip, port))
        {

        }

        public Server(IPEndPoint endpoint)
        {
            _endPoint = endpoint;
            _server = new TcpClient();
            _encoder = new ASCIIEncoding();
        }
        #endregion

        #region connectivity
        public Boolean Connect(IPEndPoint endpoint = null)
        {
            if (_server.Connected)
                return false;

            if (endpoint != null)
                _endPoint = endpoint;

            _server.Connect(_endPoint);
            return true;
        }

        public Boolean Disconnect()
        {
            if (!_server.Connected)
                return false;

            _server.Close();
            return true;
        }
        #endregion

        public Boolean Login(String user, String pass)
        {
            //sendMessage(Command.Login, MessageType.Private, user + "")
            return false;
        }

        public void Message(String message, MessageType mt = MessageType.All)
        {
            sendMessage(Command.Message, mt, message);
        }

        private void sendMessage(Command cmd, MessageType mt, String msg)
        {
            NetworkStream clientStream = _server.GetStream();
            byte[] buffer = _encoder.GetBytes(cmd + ":" + mt + ":" + msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
    }
}