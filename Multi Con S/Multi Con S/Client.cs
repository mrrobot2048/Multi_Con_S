using System;
using System.Net;
using System.Net.Sockets;

namespace Multi_Con_S
{
    class Client
    {
        public string ID
        {
            get;
            private set;

        }
        public IPEndPoint EndPoint
        {
            get;
            private set;

        }
        Socket Sck;
        //constructor
        public Client(Socket accepted)
        {
            Sck = accepted;
            ID = Guid.NewGuid().ToString();
            EndPoint = (IPEndPoint)Sck.RemoteEndPoint;
            Sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
        }
        void callback(IAsyncResult ar)
        {
            try
            {
                Sck.EndReceive(ar);
                byte[] buf = new byte[8192];
                int rec = Sck.Receive(buf, buf.Length, 0);
                if (rec < buf.Length)
                {
                    Array.Resize<byte>(ref buf, rec);
                }
                if (Received != null)
                {
                    Received(this, buf);
                }
                Sck.BeginReceive(new byte[] { 0 }, 0, 0, 0, callback, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Close();

                if (Disconnected != null)
                {
                    Disconnected(this);
                }

            }

        }
        public void Close()
        {
            Sck.Close();
            Sck.Dispose();
        }
        public delegate void ClientReceivedHandler(Client sender, byte[] data);
        public delegate void ClientDisconnectedHandler(Client sender);

        public event ClientReceivedHandler Received;
        public event ClientDisconnectedHandler Disconnected;
    }
}
