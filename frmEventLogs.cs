using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cane_Tracking.Classes;

namespace Cane_Tracking
{
    public partial class frmEventLogs : Form
    {

        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
