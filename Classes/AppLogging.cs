using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class AppLogging
    {

        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();

        private static bool StateSaved { get; set; }

        SqlConnection con;

        public void LogEvent(string log)
        {
            con = new SqlConnection(cnf.DbAddress);
            SqlCommand cmd = new SqlCommand(query.SaveLog(log), con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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

        public void SavingState(TrackingList tl, string seriesNo)
        {
            con = new SqlConnection(cnf.DbAddress);
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
                    StateSaved = true;
                }
                catch (SqlException ex)
                {
                    StateSaved = false;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public bool SavedState()
        {
            return StateSaved;
        }

        public void TruncateSavedStateLogs()
        {
            SqlCommand cmd = new SqlCommand(query.TruncateSavedStateLogs(), con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
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
}
