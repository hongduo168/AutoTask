using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTask.Helper.Model
{
    [Alias("QRTZ_SETTING")]
    public class SettingInfo
    {
        public string JOB_NAME { get; set; }
        public string JOB_GROUP { get; set; }
        public string DESCRIPTION { get; set; }
        public string TRIGGER_NAME { get; set; }
        public string TRIGGER_GROUP { get; set; }
        public string CRON_EXPRESSION { get; set; }
        public string START_TIME { get; set; }
        public string END_TIME { get; set; }
        public string HOST { get; set; }
        public string REQUESTHOST { get; set; }
        public string REQUESTPATH { get; set; }
    }
}
