using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class NirUDP
    {

        ConfigValues cnf = new ConfigValues();

        private static string Message { get; set; }

        private bool listening;


        Thread newThread;
        UdpClient udpClient;


        public NirUDP()
        {
            this.listening = false;
        }

        private void UdpSendMessage(byte[] data)
        {
            string ipAddress = cnf.NirAddress;
            int sendPort = cnf.NirPort;

            try
            {
                using (var client = new UdpClient())
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), sendPort);
                    client.Connect(ep);
                    client.Send(data, data.Length);
                }
            }
            catch (SocketException)
            {

            }
        }

        public void SendMessage(string sample)
        {
            string sendString = cnf.NirSendMessage;
            string sampleToScan = String.Concat(sendString, sample);

            byte[] data = Encoding.ASCII.GetBytes(sampleToScan);

            UdpSendMessage(data);
        }

        public void EndMessage(string sample)
        {
            string sendString = cnf.NirEndMessage;
            string sampleToScan = String.Concat(sendString, sample);

            byte[] data = Encoding.ASCII.GetBytes(sampleToScan);

            UdpSendMessage(data);
        }

        private void ListenToUDPMessage()
        {
            udpClient = null;
            int port = cnf.NirPort;


            try
            {
                udpClient = new UdpClient(port);
            }
            catch (SocketException)
            {

            }

            if(udpClient != null)
            {
                IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, port);

                try
                {
                    while (this.listening)
                    {
                        byte[] receivedMessage = udpClient.Receive(ref ipEndpoint);
                        Message = Encoding.ASCII.GetString(receivedMessage);
                    }
                }
                catch (SocketException e)
                {
                    MessageBox.Show(e.Message, "Message #1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void StartListening()
        {
            if (!this.listening)
            {
                newThread = new Thread(new ThreadStart(ListenToUDPMessage));
                newThread.IsBackground = true;
                this.listening = true;
                newThread.Start();
            }
        }

        public void StopListening()
        {
            this.listening = false;
            newThread.Abort();
            udpClient.Close();
        }

        public string GetMessage()
        {
            return Message;
        }

    }
}
