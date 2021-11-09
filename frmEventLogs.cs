using Cane_Tracking.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cane_Tracking
{
    public partial class frmEventLogs : Form
    {

        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();
        AppLogging log = new AppLogging();
        SqlConnection con;


        public frmEventLogs()
        {
            InitializeComponent();
            con = new SqlConnection(cnf.DbAddress);
        }

        private void frmEventLogs_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(query.ListEventLog(), con);

            try
            {
                con.Open();

                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("No Records found");
                    this.Close();
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvEventLogs.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.LogEvent(DateTime.Now + " : " + ex.Message.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
