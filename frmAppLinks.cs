using Cane_Tracking.Classes;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking
{
    public partial class frmAppLinks : Form
    {

        ConfigValues cnf = new ConfigValues();
        NirUDP ncs = new NirUDP();

        private static string pingStatus;
        private bool AlreadyConnected;


        public frmAppLinks(bool alreadyConnected)
        {
            this.AlreadyConnected = alreadyConnected;

            InitializeComponent();
            DefaultValues();
            ConnectUDP();
        }


        /*=================NIR NCS CONNECTION CHECK=================*/

        private void DefaultValues()
        {
            txtIpAddress.Text = cnf.NirAddress;
            txtPort.Text = cnf.NirPort.ToString();
            txtLocalPort.Text = cnf.PcPort.ToString();
            txtStartSample.Text = cnf.NirSendMessage;
            txtEndSample.Text = cnf.NirEndMessage;
        }

        private void ConnectUDP()
        {
            if (!AlreadyConnected)
            {
                ncs.StartReceivedMessages();
            }
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;

            //Sending a 32b bytes of data.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(IPAddress.Parse(txtIpAddress.Text), timeout, buffer, options);


            try
            {
                if (reply.Status == IPStatus.Success)
                {
                    pingStatus = "Address: " + reply.Address.ToString() + "\n" +
                                          "RoundTrip Time: " + reply.RoundtripTime + "\n" +
                                          "Time to live: " + reply.Options.Ttl + "\n" +
                                          "Don't fragment: " + reply.Options.DontFragment + "\n" +
                                          "Buffer size: " + reply.Buffer.Length + "\n\n" +
                                          "Ping Success";

                }
                else
                {
                    pingStatus = "Ping Failed";
                }
            }
            catch (PingException ex)
            {
                pingStatus = ex.ToString();
            }

            MessageBox.Show(pingStatus, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStartSample_Click(object sender, EventArgs e)
        {
            ncs.SendMessage(txtStartSample.Text);
            btnStartSample.SendToBack();
        }

        private void btnEndSample_Click(object sender, EventArgs e)
        {
            ncs.EndMessage(txtEndSample.Text);
            btnEndSample.SendToBack();

            lblResult.Text = ncs.GetMessage();
        }


        /*==========WEIGHBRIDGE CONNECTION CHECK===========*/

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cnf.WbAdress);

            string query = "SELECT TOP 1 * FROM tblData";

            SqlCommand cmd = new SqlCommand(query, con);

            try
            {
                con.Open();
                if(cmd.ExecuteScalar() != null)
                {
                    lblQueryMessage.Visible = true;
                    lblQueryMessage.Text = "Query Successful";
                }
                else
                {
                    lblQueryMessage.Visible = true;
                    lblQueryMessage.Text = "Check Database Connection";
                }
            }
            catch(SqlException ex)
            {
                lblQueryMessage.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }

    }
}
