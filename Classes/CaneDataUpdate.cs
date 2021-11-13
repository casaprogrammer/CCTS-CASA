using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class CaneDataUpdate
    {
        Queries query = new Queries();
        AppLogging log = new AppLogging();
        ConfigValues cnf = new ConfigValues();
        CrossThreadingCheck cc = new CrossThreadingCheck();
        SqlConnection con;


        public CaneDataUpdate()
        {
            con = new SqlConnection(cnf.WbAdress);
        }

        public void GetCaneData(DataGridView dgv, DateTimePicker dtp, RichTextBox rt, RichTextBox rtLeaves)
        {
            SqlCommand cmd = new SqlCommand(query.GetBatchNumberData(cc.DateTimePickerVal(dtp), cc.GetControlValue(rt)), con);

            if (con.State != ConnectionState.Open)
            {
                try
                {
                    con.Open();

                    if (cmd.ExecuteScalar() == null)
                    {
                        MessageBox.Show("Batch #" + cc.GetControlValue(rt) + " data not found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cc.ChangeText(rt, "");
                        cc.DataGrid(dgv, null);
                    }
                    else
                    {
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        cc.DataGrid(dgv, dt);

                        cc.SetRichTextboxFocus(rtLeaves);
                        cc.SelectAllTextbox(rtLeaves);
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
        }

        public void UpdateCaneData(DataGridView dgv, RichTextBox trash, RichTextBox bitLeaves, RichTextBox bitCaneTops,
                                   RichTextBox bitRoots, RichTextBox bitDeadStalks, RichTextBox bitMixedBurned,
                                   RichTextBox bitBurned, RichTextBox bitMud, RichTextBox batchNo)
        {

            int id = int.Parse(cc.DataGridValues(dgv, 0, "ID"));
            string transCode = cc.DataGridValues(dgv, 0, "Trans Code");
            string plateNo = cc.DataGridValues(dgv, 0, "Plate No");
            double trashVal = double.Parse(cc.GetControlValue(trash));
            double leaves = double.Parse(cc.GetControlValue(bitLeaves));
            double caneTops = double.Parse(cc.GetControlValue(bitCaneTops));
            double roots = double.Parse(cc.GetControlValue(bitRoots));
            double deadStalks = double.Parse(cc.GetControlValue(bitDeadStalks));
            double mixedBurned = double.Parse(cc.GetControlValue(bitMixedBurned));
            double burned = double.Parse(cc.GetControlValue(bitBurned));
            double mud = double.Parse(cc.GetControlValue(bitMud));

            SqlCommand cmd = new SqlCommand(query.UpdateCaneData(id, trashVal, leaves, caneTops, roots, deadStalks,
                                                                 mixedBurned, burned, mud
                                                                 ), con);

            try
            {
                con.Open();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Trash Discount Saved", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                log.LogEvent("");
                log.LogEvent("+===============TRASH UPDATE===============+");
                log.LogEvent(DateTime.Now.ToString());
                log.LogEvent("-------------------------------");
                log.LogEvent("Batch No: " + cc.GetControlValue(batchNo));
                log.LogEvent("Trans Code: " + transCode);
                log.LogEvent("Plate No: " + plateNo);
                log.LogEvent("-------------------------------");
                log.LogEvent("Leaves: " + cc.GetControlValue(bitLeaves));
                log.LogEvent("Cane Tops: " + cc.GetControlValue(bitCaneTops));
                log.LogEvent("Roots: " + cc.GetControlValue(bitRoots));
                log.LogEvent("Dead Stalks: " + cc.GetControlValue(bitDeadStalks));
                log.LogEvent("Mixed Burned: " + cc.GetControlValue(bitMixedBurned));
                log.LogEvent("Burned: " + cc.GetControlValue(bitBurned));
                log.LogEvent("Mud: " + cc.GetControlValue(bitMud));
                log.LogEvent("Total Trash: " + cc.GetControlValue(trash));
                log.LogEvent("+=============END TRASH UDPATE=============+");
                log.LogEvent("");


                batchNo.Text = "";
                cc.ChangeText(batchNo, "");
                cc.DataGrid(dgv, null);

                cc.ChangeText(trash, "0");
                cc.ChangeText(bitLeaves, "0");
                cc.ChangeText(bitCaneTops, "0");
                cc.ChangeText(bitRoots, "0");
                cc.ChangeText(bitDeadStalks, "0");
                cc.ChangeText(bitMixedBurned, "0");
                cc.ChangeText(bitBurned, "0");
                cc.ChangeText(bitMud, "0");

                cc.SelectionAlignmentRichTextbox(trash);
                cc.SelectionAlignmentRichTextbox(bitLeaves);
                cc.SelectionAlignmentRichTextbox(bitCaneTops);
                cc.SelectionAlignmentRichTextbox(bitRoots);
                cc.SelectionAlignmentRichTextbox(bitDeadStalks);
                cc.SelectionAlignmentRichTextbox(bitMixedBurned);
                cc.SelectionAlignmentRichTextbox(bitBurned);
                cc.SelectionAlignmentRichTextbox(bitMud);

            }
            catch (SqlException ex)
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
