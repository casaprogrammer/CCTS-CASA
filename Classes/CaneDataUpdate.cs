using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class CaneDataUpdate
    {
        Queries query = new Queries();
        AppLogging log = new AppLogging();
        ConfigValues cnf = new ConfigValues();
        SqlConnection con;

        public CaneDataUpdate()
        {
            con = new SqlConnection(cnf.WbAdress);
        }

        public void GetCaneData(DataGridView dgv, DateTimePicker dtp, RichTextBox rt, RichTextBox rtLeaves, Button btnSave, Button btnCancel)
        {
            SqlCommand cmd = new SqlCommand(query.GetBatchNumberData(dtp.Value.ToString("yyyy-MM-dd"), rt.Text), con);

            try
            {
                con.Open();

                if (cmd.ExecuteScalar() == null)
                {
                    MessageBox.Show("Batch #" + rt.Text + " data not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rt.Text = "";
                    dgv.DataSource = null;
                }
                else
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv.DataSource = dt;
                    dgv.Columns[0].Visible = false;

                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;

                    rtLeaves.Focus();
                    rtLeaves.SelectAll();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateCaneData(DataGridView dgv, RichTextBox trash, RichTextBox bitLeaves, RichTextBox bitCaneTops,
                                   RichTextBox bitRoots, RichTextBox bitDeadStalks, RichTextBox bitMixedBurned,
                                   RichTextBox bitBurned, RichTextBox bitMud, RichTextBox batchNo)
        {

            int id = int.Parse(dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells["ID"].Value.ToString());
            double trashVal = double.Parse(trash.Text);
            double leaves = double.Parse(bitLeaves.Text);
            double caneTops = double.Parse(bitCaneTops.Text);
            double roots = double.Parse(bitRoots.Text);
            double deadStalks = double.Parse(bitDeadStalks.Text);
            double mixedBurned = double.Parse(bitMixedBurned.Text);
            double burned = double.Parse(bitBurned.Text);
            double mud = double.Parse(bitMud.Text);

            SqlCommand cmd = new SqlCommand(query.UpdateCaneData(id, trashVal, leaves, caneTops, roots, deadStalks,
                                                                 mixedBurned, burned, mud
                                                                 ), con);

            try
            {
                con.Open();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Trash Discount Saved", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                log.AppEventLog("");
                log.AppEventLog("+===============TRASH UPDATE===============+");
                log.AppEventLog(DateTime.Now.ToString());
                log.AppEventLog("Batch No: " + batchNo.Text);
                log.AppEventLog("Leaves: " + bitLeaves.Text);
                log.AppEventLog("Cane Tops: " + bitCaneTops.Text);
                log.AppEventLog("Roots: " + bitRoots.Text);
                log.AppEventLog("Dead Stalks: " + bitDeadStalks.Text);
                log.AppEventLog("Mixed Burned: " + bitMixedBurned.Text);
                log.AppEventLog("Burned: " + bitBurned.Text);
                log.AppEventLog("Mud: " + bitMud.Text);
                log.AppEventLog("+=============END TRASH UDPATE=============+");
                log.AppEventLog("");


                batchNo.Text = "";
                dgv.DataSource = null;

                trash.Text = "0";
                bitLeaves.Text = "0";
                bitCaneTops.Text = "0";
                bitRoots.Text = "0";
                bitDeadStalks.Text = "0";
                bitMixedBurned.Text = "0";
                bitBurned.Text = "0";
                bitMud.Text = "0";

                trash.SelectionAlignment = HorizontalAlignment.Right;
                bitLeaves.SelectionAlignment = HorizontalAlignment.Right;
                bitCaneTops.SelectionAlignment = HorizontalAlignment.Right;
                bitRoots.SelectionAlignment = HorizontalAlignment.Right;
                bitDeadStalks.SelectionAlignment = HorizontalAlignment.Right;
                bitMixedBurned.SelectionAlignment = HorizontalAlignment.Right;
                bitBurned.SelectionAlignment = HorizontalAlignment.Right;
                bitMud.SelectionAlignment = HorizontalAlignment.Right;

            }
            catch(SqlException ex)
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
