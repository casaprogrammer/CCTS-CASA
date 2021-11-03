using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class AppLogging
    {

        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();

        SqlConnection con;

        private static bool State_is_saved { get; set; }
        private static bool Db_Connected { get; set; }

        public AppLogging()
        {
            con = new SqlConnection(cnf.DbAddress);
        }

        public void AppEventLog(string log)
        {
            SqlCommand cmd = new SqlCommand(query.SaveLog(log), con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        public void SavingState(TrackingList tl, string seriesNo)
        {
            for (int i = 0; i < tl.lTbox.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(query.SaveStateLog
                                                                  (tl.lTbox[i].Item1.Text,
                                                                   tl.lTbox[i].Item2.Text,
                                                                   tl.lTbox[i].Item3,
                                                                   DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                                                                   seriesNo),
                                                                   con);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    State_is_saved = true;
                }
                catch (Exception)
                {
                    State_is_saved = false;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public bool StateSaved()
        {
            return State_is_saved;
        }

        public bool DbConnected()
        {
            try
            {
                con.Open();
                Db_Connected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            return Db_Connected;
        }

        public void TruncateSavedStateLogs()
        {
            SqlCommand cmd = new SqlCommand(query.TruncateSavedStateLogs(), con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
