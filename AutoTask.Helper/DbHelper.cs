using AutoTask.Helper.Model;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper
{
    public class DbHelper
    {
        private static IDbConnectionFactory dbFactory = null;
        static DbHelper()
        {
            if (dbFactory == null)
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbQuartz"].ConnectionString;

                dbFactory = new OrmLiteConnectionFactory(
                    connectionString,
                    SqlServer2012Dialect.Provider);
            }
        }

        #region setting表
        public static SettingInfo GetSetting(string name, string group)
        {
            using (var db = dbFactory.Open())
            {
                var result = db.Single(db.From<SettingInfo>().Where(q => q.JOB_NAME == name && q.JOB_GROUP == group));
                return result;
            }
        }

        public static int InsertSetting(SettingInfo data)
        {
            using (var db = dbFactory.Open())
            {
                var result = db.Insert(data);
                return (int)result;
            }
        }

        public static int UpdateSetting(SettingInfo data)
        {
            using (var db = dbFactory.Open())
            {
                var result = db.UpdateNonDefaults(new SettingInfo
                {
                    DESCRIPTION = data.DESCRIPTION,
                    CRON_EXPRESSION = data.CRON_EXPRESSION,
                    START_TIME = data.START_TIME,
                    END_TIME = data.END_TIME,
                    REQUESTHOST = data.REQUESTHOST,
                    REQUESTPATH = data.REQUESTPATH
                },
                q => q.JOB_NAME == data.JOB_NAME && q.JOB_GROUP == data.JOB_GROUP);
                return result;
            }

        }

        public static List<SettingInfo> GetSettingAll()
        {
            using (var db = dbFactory.Open())
            {
                var result = db.Select<SettingInfo>(db.From<SettingInfo>().OrderBy(q => q.JOB_GROUP));
                return result;
            }
        }
        #endregion
        /// <summary>
        /// 清理过期数据
        /// </summary>
        public static void ExpiredDataClear()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM [QRTZ_BLOB_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_CALENDARS]");
            sb.AppendLine("DELETE FROM [QRTZ_CRON_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_FIRED_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_LOCKS]");
            sb.AppendLine("DELETE FROM [QRTZ_PAUSED_TRIGGER_GRPS]");
            sb.AppendLine("DELETE FROM [QRTZ_SCHEDULER_STATE]");
            sb.AppendLine("DELETE FROM [QRTZ_SIMPLE_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_SIMPROP_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_TRIGGERS]");
            sb.AppendLine("DELETE FROM [QRTZ_JOB_DETAILS]");


            using (var db = dbFactory.Open())
            {
                var result = db.ExecuteNonQuery(sb.ToString());
            }
        }
    }

}




