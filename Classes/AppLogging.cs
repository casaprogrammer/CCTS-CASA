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


        private static bool State_is_saved { get; set; }
        private static bool Db_Exist { get; set; }
        private static bool App_Db_Connected { get; set; }
        private static bool Wb_Db_Connected { get; set; }

        SqlConnection con;

        public void AppEventLog(string log)
        {
            if (App_Db_Connected)
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
        }

        public void SavingState(TrackingList tl, string seriesNo)
        {
            if (App_Db_Connected)
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
                        State_is_saved = true;
                    }
                    catch (SqlException ex)
                    {
                        State_is_saved = false;
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        public bool StateSaved()
        {
            return State_is_saved;
        }

        public bool DbConnectionExist()
        {
            SqlConnection appCon = new SqlConnection(cnf.DefaultConnection);

            try
            {
                appCon.Open();
                Db_Exist = true;
                DbConnect();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                appCon.Close();
            }

            return Db_Exist;
        }

        public bool DbConnect()
        {
            SqlConnection appCon = new SqlConnection(cnf.DbAddress);

            try
            {
                appCon.Open();
                App_Db_Connected = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("canetracking table not found, App will create a table", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CreateDB();
            }
            finally
            {
                appCon.Close();
            }

            return App_Db_Connected;
        }

        public bool WbDBConnected()
        {
            SqlConnection appCon = new SqlConnection(cnf.WbAdress);

            try
            {
                appCon.Open();
                Wb_Db_Connected = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                appCon.Close();
            }

            return Wb_Db_Connected;
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

        private void CreateDB()
        {
            SqlConnection newCon = new SqlConnection(cnf.DefaultConnection);
            SqlCommand cmd = new SqlCommand(query.CreateAppDB(), newCon);

            try
            {
                newCon.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                CreateAppLogsTable();
                newCon.Close();
            }
        }

        private void CreateAppLogsTable()
        {
            SqlConnection newCon = new SqlConnection(cnf.DefaultConnection);
            SqlCommand cmd = new SqlCommand(query.CreateTable(), newCon);

            try
            {
                newCon.Open();
                cmd.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                DbConnect();
                newCon.Close();
            }
        }

        public bool DbConnectionSuccess()
        {
            return App_Db_Connected;
        }
    }
}
