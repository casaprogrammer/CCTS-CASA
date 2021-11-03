using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cane_Tracking.Classes
{
    class NirUDP
    {

        ConfigValues cnf = new ConfigValues();

        private static bool Udp_connected { get; set; }

        public bool UdpConnected()
        {
            try
            {
                string ipAddress = cnf.NirAddress;
                int sendPort = cnf.NirPort;

                using(var client = new UdpClient())
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ipAddress), sendPort);
                    client.Connect(ep);

                    Udp_connected = true;
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.ToString());
                Udp_connected = false;
            }

            return Udp_connected;
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
