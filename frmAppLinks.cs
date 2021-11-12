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
        PingPC pingPC = new PingPC();
        NirUDP ncs = new NirUDP();

        public frmAppLinks()
        {
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
            ncs.StartListening();
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            pingPC.PingNir();

            if (pingPC.GetPingStatus())
            {
                MessageBox.Show(pingPC.GetPingResult(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ping Failed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
                if (cmd.ExecuteScalar() != null)
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
            catch (SqlException ex)
            {
                lblQueryMessage.Text = ex.ToString();
            }
            finally
            {
                con.Close();
            }
        }

        private void frmAppLinks_FormClosed(object sender, FormClosedEventArgs e)
        {
            ncs.StopListening();
        }
    }
}
