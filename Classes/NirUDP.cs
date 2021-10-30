using System;
using System.Collections.Generic;
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
            catch (Exception)
            {
                Udp_connected = false;
            }

            return Udp_connected;
        }
    }
}
