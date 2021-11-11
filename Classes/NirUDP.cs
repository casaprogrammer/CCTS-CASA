using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    public struct UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }

    class NirUDP
    {

        ConfigValues cnf = new ConfigValues();

        private static bool PingSuccess { get; set; }
        private static string Message { get; set; }

        public static bool messageReceived = false;


        ThreadStart threadStart;
        Thread newThread;

        public NirUDP()
        {
            threadStart = new ThreadStart(ReceiveMessages);
            newThread = new Thread(threadStart);
        }

        public void PingPC()
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
                    PingSuccess = true;
                }
            }
            catch (PingException ex)
            {
                MessageBox.Show(ex.Message);
                PingSuccess = false;
            }
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

        public void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = ((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = ((UdpState)(ar.AsyncState)).e;

            byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Message = receiveString;
            messageReceived = true;
        }

        public void ReceiveMessages()
        {
            int listenPort = cnf.NirPort;

            try
            {
                IPEndPoint e = new IPEndPoint(IPAddress.Any, listenPort);
                UdpClient u = new UdpClient(e);

                UdpState s = new UdpState();
                s.e = e;
                s.u = u;

                Message = "Waiting for result";
                u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

                // Do some work while we wait for a message. For this example, we'll just sleep
                while (!messageReceived)
                {
                    Thread.Sleep(100);
                }

            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void StartReceivedMessages()
        {
            newThread.Start();
        }

        public void StopReceivingThread()
        {

        }

        public string GetMessage()
        {
            return Message;
        }

        public bool PingResult()
        {
            return PingSuccess;
        }
    }
}
