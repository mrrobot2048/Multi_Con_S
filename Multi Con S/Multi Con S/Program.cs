using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Multi_Con_S
{
    class Program
    {
        static Listener l;
        static List<Socket> sockets;
        static void Main(string[] args)
        {
            l = new Listener(8);
            l.SocketAccepted += L_SocketAccepted;
            l.Start();
            sockets = new List<Socket>();

            Console.Read();
        }

        private static void L_SocketAccepted(Socket e)
        {
            Console.WriteLine("New connection. {0}\n{1}\n==============", e.RemoteEndPoint, DateTime.Now);
            sockets.Add(e);
        }
    }
}
