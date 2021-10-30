using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class LoadingValues
    {
        Queries query = new Queries();
        NirTimer timers = new NirTimer();
        ConfigValues cnf = new ConfigValues();

        SqlConnection con;

        private static bool Data_is_loaded { get; set; }

        private static string val { get; set; }

        public LoadingValues()
        {
            con = new SqlConnection(cnf.DbAddress);
        }

        private bool ValuesLoaded()
        {
            SqlCommand cmd = new SqlCommand(query.CheckStatus(), con);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (rdr["current_status"].ToString() == "1")
                    {
                        Data_is_loaded = true;
                    }
                    else
                    {
                        Data_is_loaded = false;
                    }
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

            return Data_is_loaded;
        }

        private void StatusValue()
        {
            SqlCommand cmd = new SqlCommand(query.CheckStatus(), con);

            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    if (Data_is_loaded)
                    {
                        val = rdr["status_val"].ToString();
                    }
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

        private void LoadBatchNumber(TrackingList bnlist)
        {
            string queryString = @"SELECT batchNumber, currentCount, areaName, 
                                   (SELECT MAX(seriesNo) FROM saved_state_logs WHERE dateSaved = @dateSaved) as [series_no]
                                   FROM saved_state_logs WHERE dateSaved = @dateSaved";

            try
            {
                SqlCommand cmd = new SqlCommand(queryString, con);

                cmd.Parameters.AddWithValue("@dateSaved", val);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();

                for (int i = 0; i < bnlist.lTbox.Count; i++)
                {
                    bnlist.lTbox[i].Item1.Text = "";
                    bnlist.lTbox[i].Item2.Text = "";
                    bnlist.lTbox[i].Item2.BackColor = Color.CornflowerBlue;

                    bnlist.dumpCanesHistory.Clear();
                    bnlist.tipperOne.Clear();
                    bnlist.tipperTwo.Clear();
                    bnlist.dumpTruck.Clear();
                    bnlist.stockPile.Clear();
                    bnlist.mainCane.Clear();
                    bnlist.caneKnives.Clear();
                    bnlist.shreddedCane.Clear();
                    timers.washingTimerList.Clear();
                    timers.nirTimerList.Clear();
                }

                while (rdr.Read())
                {
                    for (int i = 0; i < bnlist.lTbox.Count; i++)
                    {
                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "TipOne"
                            && rdr["areaName"].ToString() == "TipOne")
                        {
                            bnlist.tipperOne.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Tipper One"));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "TipTwo"
                            && rdr["areaName"].ToString() == "TipTwo")
                        {
                            bnlist.tipperTwo.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Tipper Two"));

                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "DumpTruck"
                            && rdr["areaName"].ToString() == "DumpTruck")
                        {
                            bnlist.dumpTruck.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Dump Truck"));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "StockPile"
                            && rdr["areaName"].ToString() == "StockPile")
                        {
                            bnlist.stockPile.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.dumpCanesHistory.Add(new Tuple<RichTextBox, RichTextBox, string>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2, "Stock Pile"));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "MainCane"
                           && rdr["areaName"].ToString() == "MaineCane")
                        {
                            bnlist.mainCane.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }


                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "CaneKnives"
                          && rdr["areaName"].ToString() == "CaneKnives")
                        {
                            bnlist.caneKnives.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "Shredder"
                          && rdr["areaName"].ToString() == "Shredder")
                        {
                            bnlist.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }

                        /*if (bnlist.lTbox[i].Item1.Text == "" && bnlist.lTbox[i].Item2.Text == "" && bnlist.lTbox[i].Item3 == "Nir"
                          && rdr["areaName"].ToString() == "Nir")
                        {
                            bnlist.shreddedCane.Add(new Tuple<RichTextBox, RichTextBox>(bnlist.lTbox[i].Item1, bnlist.lTbox[i].Item2));
                            bnlist.lTbox[i].Item1.Text = rdr["batchNumber"].ToString();
                            bnlist.lTbox[i].Item2.Text = rdr["currentCount"].ToString();

                            if (bnlist.lTbox[i].Item1.Text != "")
                            {
                                bnlist.lTbox[i].Item2.BackColor = Color.Maroon;
                                bnlist.lTbox[i].Item2.ForeColor = Color.White;
                            }
                            break;
                        }*/
                    }
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

        public void LoadSavedValues(TrackingList bnlist)
        {
            ValuesLoaded();
            StatusValue();

            if (Data_is_loaded)
            {
                LoadBatchNumber(bnlist);  
            }
            ResetLoadValues();
        }

        public void ResetLoadValues()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(query.UpdateStatus(), con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
