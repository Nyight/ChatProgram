using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatProgramServer
{
    class TCPServer
    {
        private TcpListener _tcpListener;
        private Thread _listeningThread;
        private ASCIIEncoding _encoder;
        private ThreadSafeList<TcpClient> _clients;

        private volatile bool _stop;

        public TCPServer()
        {
            _tcpListener = new TcpListener(IPAddress.Any, 3000);
            _listeningThread = new Thread(new ThreadStart(ListenForClients));
            _clients = new ThreadSafeList<TcpClient>();

            _stop = false;
            _encoder = new ASCIIEncoding();
        }

        public void Start()
        {
            _listeningThread.Start();
            //_listeningThread.Join();
        }

        public void Stop()
        {
            _stop = true;
        }

        private void ListenForClients()
        {
            _tcpListener.Start();

            while(!_stop)
            {
                // blocks until a client has connected to the server
                TcpClient client = _tcpListener.AcceptTcpClient();

                // create a thread to handle communication with connected client
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);

                _clients.Add(client);
            }
        }

        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] message = new byte[4096];
            int bytesRead;

            while(true)
            {
                bytesRead = 0;

                try
                {
                    // blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch(Exception e)
                {
                    // a socket error has occured
                    Console.WriteLine("A socket error has occured." + Environment.NewLine + e.Message);
                    break;
                }

                if(bytesRead == 0)
                {
                    // the client has disconnected from the server
                    break;
                }

                // message has successfully been received
                ASCIIEncoding encoder = new ASCIIEncoding();
                Console.WriteLine(encoder.GetString(message, 0, bytesRead));
            }

            _clients.Remove(tcpClient);
            Console.WriteLine("client disconnected");
            tcpClient.Close();
        }

        private void BroacastMessage(String message)
        {
            TcpClient[] clients = new TcpClient[_clients.Count];
            _clients.CopyTo(clients, 0);

            foreach(TcpClient client in _clients)
            {
                NetworkStream clientStream = client.GetStream();
                byte[] buffer = _encoder.GetBytes(message);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
        }
    }
}
