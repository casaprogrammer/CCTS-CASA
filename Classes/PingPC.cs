using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class PingPC
    {
        ConfigValues cnf = new ConfigValues();
        
        private static bool CanPing { get; set; }
        private static string PingResult { get; set; }

        public void PingNir()
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;

            //Sending a 32b bytes of data.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(IPAddress.Parse(cnf.NirAddress), timeout, buffer, options);

            try
            {
                if (reply.Status == IPStatus.Success)
                {
                    CanPing = true;
                    PingResult = "Address: " + reply.Address.ToString() + "\n" +
                                          "RoundTrip Time: " + reply.RoundtripTime + "\n" +
                                          "Time to live: " + reply.Options.Ttl + "\n" +
                                          "Don't fragment: " + reply.Options.DontFragment + "\n" +
                                          "Buffer size: " + reply.Buffer.Length + "\n\n" +
                                          "Ping Success";
                }
            }
            catch (PingException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetPingResult()
        {
            return PingResult;
        }

        public bool GetPingStatus()
        {
            return CanPing;
        }
    }
}
