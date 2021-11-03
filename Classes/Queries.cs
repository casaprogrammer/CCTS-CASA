using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            this.query = this.query.Replace("{0}", data);

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
            this.query = "SELECT CONVERT(varchar,date_saved,20) as [Date and Time] FROM saved_state_logs GROUP BY dateSaved ORDER BY dateSaved ASC";

            return this.query;
        }

        public string ListEventLog()
        {
            this.query = "SELECT log_description as [Log Description] FROM app_logs";

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
    }
}
