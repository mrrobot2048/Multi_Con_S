using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multi_Con_S
{
    public partial class Main : Form
    {
        Listener listener;
        public Main()
        {
            InitializeComponent();
            listener = new Listener(8);
            listener.SocketAccepted += Listener_SocketAccepted;
        }

        private void Listener_SocketAccepted(System.Net.Sockets.Socket e)
        {
            Client client = new Client(e);
            client.Received += Client_Received;
            client.Disconnected += Client_Disconnected;

            Invoke((MethodInvoker)delegate
           {
               ListViewItem i = new ListViewItem();
               i.Text = client.EndPoint.ToString();
               i.SubItems.Add(client.ID);
               i.SubItems.Add("xx");
               i.SubItems.Add("xx");
               lstClients.Items.Add(i);

           });
        }

        private void Client_Disconnected(Client sender)
        {
            
        }

        private void Client_Received(Client sender, byte[] data)
        {
            
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }
}
