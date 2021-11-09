using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cane_Tracking.Classes
{
    class NirUDP
    {

        ConfigValues cnf = new ConfigValues();

        private static bool IsConnected { get; set; }

        public bool UdpConnected()
        {
            try
            {
                string ipAddress = cnf.NirAddress;
                int sendPort = cnf.NirPort;

                using (var client = new UdpClient())
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), sendPort);
                    client.Connect(ep);

                    IsConnected = true;
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
                IsConnected = false;
            }

            return IsConnected;
        }

        private void UdpSendMessage(byte[] data)
        {
            try
            {
                string ipAddress = cnf.NirAddress;
                int sendPort = cnf.NirPort;

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
    }
}
