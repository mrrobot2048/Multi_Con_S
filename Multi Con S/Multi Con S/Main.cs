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
            Load += Main_Load;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            listener.Start();
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
                i.SubItems.Add("xx");
                i.SubItems.Add("xx");
                i.SubItems.Add("xx");
                i.Tag = client;
                lstClients.Items.Add(i);

            });
        }
        private void Client_Disconnected(Client sender)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    Client client = lstClients.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {
                        lstClients.Items.RemoveAt(i);
                        break;
                    }
                }

            });
        }

        private void Client_Received(Client sender, byte[] data)
        {
            Invoke((MethodInvoker)delegate
            {
                for (int i = 0; i < lstClients.Items.Count; i++)
                {
                    Client client = lstClients.Items[i].Tag as Client;
                    if (client.ID == sender.ID)
                    {

                        String decoded = Encoding.Default.GetString(data);

                        string[] lines = decoded.Split('\n');
                        string MName = lines[0];
                        string UName = lines[1];
                        string OS = lines[2];
                        string AV = lines[3];

                        lstClients.Items[i].SubItems[2].Text = MName;
                        lstClients.Items[i].SubItems[2].Text = UName;
                        lstClients.Items[i].SubItems[3].Text = OS;

                        break;
                    }
                }

            });
        }
    }
}
