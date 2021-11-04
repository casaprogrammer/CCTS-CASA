using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cane_Tracking.Classes;

namespace Cane_Tracking
{
    public partial class frmLoadData : Form
    {

        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();
        AppLogging log = new AppLogging();
        SqlConnection con;


        public frmLoadData()
        {
            InitializeComponent();
            con = new SqlConnection(cnf.DbAddress);
        }

        private void frmStateLogs_Load(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand(query.ListStatusLog(), con);

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
                    dgvStateLogs.DataSource = dt;
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

        private void dgvStateLogs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                string val = dgvStateLogs.Rows[e.RowIndex].Cells["Date and Time"].FormattedValue.ToString();

                DialogResult dialogResult = MessageBox.Show("Proceed loading selected data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                    SqlCommand cmd = new SqlCommand(query.LoadedStatus(val), con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                        string t = DateTime.Now + " : " + "Loaded saved state data";
                        log.AppEventLog(t);
                    }
                    catch(SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        log.AppEventLog(DateTime.Now + " : " + ex.Message.ToString());
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }
    }
}
