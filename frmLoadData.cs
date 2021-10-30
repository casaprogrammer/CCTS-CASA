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
        LoadingValues appLoading = new LoadingValues();
        SqlConnection con = new SqlConnection(File.ReadAllText(Path.GetFullPath("Configurations/DbConnection.txt")));


        public frmLoadData()
        {
            InitializeComponent();
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
                string val = dgvStateLogs.Rows[e.RowIndex].Cells["Saved State Logs"].FormattedValue.ToString();

                DialogResult dialogResult = MessageBox.Show("Proceed loading selected data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {

                    SqlCommand cmd = new SqlCommand(query.LoadedStatus(val), con);
                    try
                    {
                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch(Exception ex)
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
    }
}
