using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cane_Tracking.Classes
{
    class ConnectionsCheck
    {
        Queries query = new Queries();
        ConfigValues cnf = new ConfigValues();

        private static bool HasConnection { get; set; }
        private static bool AppDatabaseConnected { get; set; }
        private static bool WeighBridgeDatabaseConnected { get; set; }

        private static bool HasBeenCatch;
        public bool ConnectionExist()
        {
            SqlConnection appCon = new SqlConnection(cnf.DefaultConnection);

            try
            {
                appCon.Open();
                HasConnection = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally{
                appCon.Close();
            }

            return HasConnection;
        }

        public bool WeighBridgeConnectionExist()
        {
            SqlConnection appCon = new SqlConnection(cnf.WbAdress);
            string query = "SELECT TOP 3 * FROM tblData"; //Just for testing
            SqlCommand cmd = new SqlCommand(query, appCon);

            try
            {
                appCon.Open();
                cmd.ExecuteNonQuery();

                WeighBridgeDatabaseConnected = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("WeighBridge Database does not exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                appCon.Close();
            }

            return WeighBridgeDatabaseConnected;
        }

        public void CheckConnectionDatabase()
        {
            SqlConnection appCon = new SqlConnection(cnf.DbAddress);

            try
            {
                appCon.Open();
                AppDatabaseConnected = true;
            }
            catch (SqlException)
            {
                if (!HasBeenCatch)
                {
                    MessageBox.Show("App Creating Database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CreateDatabase();

                    HasBeenCatch = true;
                }
            }
            finally
            {
                appCon.Close();
            }
        }

        public bool DbConnectionEstablished()
        {
            return AppDatabaseConnected;
        }

        private void CreateDatabase()
        {
            SqlConnection newCon = new SqlConnection(cnf.DefaultConnection);
            SqlCommand cmd = new SqlCommand(query.CreateAppDB(), newCon);

            try
            {
                newCon.Open();
                cmd.ExecuteNonQuery();
                CreateDatabaseTables();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                newCon.Close();
            }
        }

        private void CreateDatabaseTables()
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
                newCon.Close();
            }
        }
    }
}
