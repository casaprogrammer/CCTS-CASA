namespace Cane_Tracking.Classes
{
    class Queries
    {

        private string query { get; set; }

        public string SaveLog(string data)
        {
            this.query = @"INSERT INTO app_logs
                           (log_description)
                           VALUES
                           ('{0}')";

            this.query = this.query.Replace("{0}", data.Replace("'", ""));

            return this.query;
        }

        public string SaveStateLog(string batchNum, string currentCount, string areaName, string dateSaved, string seriesNo)
        {
            this.query = @"INSERT INTO saved_state_logs
                           (batch_number, current_count, area_name, date_saved, series_no)
                           VALUES
                           ('{0}', '{1}', '{2}', '{3}', '{4}')";

            this.query = this.query.Replace("{0}", batchNum);
            this.query = this.query.Replace("{1}", currentCount);
            this.query = this.query.Replace("{2}", areaName);
            this.query = this.query.Replace("{3}", dateSaved);
            this.query = this.query.Replace("{4}", seriesNo);

            return this.query;
        }

        public string LoadData(string data)
        {
            this.query = @"SELECT batch_number, current_count, area_name
                           FROM saved_state_logs WHERE date_saved = '{2}'";

            this.query = this.query.Replace("{0}", data);
            this.query = this.query.Replace("{1}", data);
            this.query = this.query.Replace("{2}", data);

            return this.query;
        }

        public string ListStatusLog()
        {
            this.query = "SELECT CONVERT(varchar,date_saved,20) as [Date and Time] FROM saved_state_logs GROUP BY date_saved ORDER BY date_saved ASC";

            return this.query;
        }

        public string ListEventLog()
        {
            this.query = "SELECT log_description as [List of Events] FROM app_logs";

            return this.query;
        }

        public string LoadedStatus(string data)
        {
            this.query = @"UPDATE app_status
                           SET current_status = 1,
                               status_value = '{0}'
                           WHERE current_status = 0";
            this.query = this.query.Replace("{0}", data);

            return this.query;
        }

        public string UpdateStatus()
        {
            this.query = @"UPDATE app_status
                           SET current_status = 0,
                               status_value = ''
                           WHERE current_status = 1";

            return this.query;
        }

        public string CheckStatus()
        {
            this.query = "SELECT current_status, CONVERT(varchar,status_value,20) as [status_val] FROM app_status";

            return this.query;
        }

        public string TruncateSavedStateLogs()
        {
            this.query = "TRUNCATE TABLE saved_state_logs";

            return this.query;
        }

        public string GetBatchNumberData(string date, string batchNumber)
        {
            this.query = @"SELECT ID, DateIn, GatePass as [Trans Code], PlateNo as [Plate No]
                           FROM tblData
                           WHERE DateIn = '{0}'
                           AND BatchNo = '{1}'";

            this.query = this.query.Replace("{0}", date);
            this.query = this.query.Replace("{1}", batchNumber);

            return this.query;
        }

        public string UpdateCaneData(int id, double trash,
                                     double bitLeaves, double bitCaneTops,
                                     double bitRoots, double bitDeadStsalks,
                                     double bitMixedBurned, double bitBurned,
                                     double bitMud)
        {
            this.query = @"UPDATE tblData
                           SET Trash = '{0}',
                               bitLeaves = '{1}',
                               bitCaneTops = '{2}',
                               bitRoots = '{3}',
                               bitDeadStalks = '{4}',
                               bitMixedBurned = '{5}',
                               bitBurned = '{6}',
                               bitMud = '{7}'
                           WHERE ID = '{8}'";

            this.query = this.query.Replace("{0}", trash.ToString());
            this.query = this.query.Replace("{1}", bitLeaves.ToString());
            this.query = this.query.Replace("{2}", bitCaneTops.ToString());
            this.query = this.query.Replace("{3}", bitRoots.ToString());
            this.query = this.query.Replace("{4}", bitDeadStsalks.ToString());
            this.query = this.query.Replace("{5}", bitMixedBurned.ToString());
            this.query = this.query.Replace("{6}", bitBurned.ToString());
            this.query = this.query.Replace("{7}", bitMud.ToString());
            this.query = this.query.Replace("{8}", id.ToString());

            return this.query;
        }

        public string CreateAppDB()
        {
            this.query = "CREATE DATABASE canetracking;";

            return this.query;
        }

        public string CreateTable()
        {
            this.query = @"USE canetracking;

                           CREATE TABLE app_logs
                           (
                                id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                                log_description nvarchar(max) 
                           );

                           CREATE TABLE app_status
                           (
                                id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                                current_status smallint,
                                status_value datetime  
                           );

                           CREATE TABLE saved_state_logs
                           (
                                id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
                                batch_number nvarchar(max),
                                current_count nvarchar(max),
                                area_name nvarchar(max),
                                series_no nvarchar(max),
                                date_saved datetime
                           );

                           INSERT INTO app_status
                           (current_status)
                           VALUES
                           ('0');

                            ";

            return this.query;
        }
    }
}
