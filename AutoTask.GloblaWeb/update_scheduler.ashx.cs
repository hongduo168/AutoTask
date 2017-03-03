
using AutoTask.Helper;
using AutoTask.Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoTask.GloblaWeb
{
    /// <summary>
    /// update_scheduler 的摘要说明
    /// </summary>
    public class update_scheduler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated) return;

            //context.Response.ContentType = "application/json";
            var jobName = context.Request.Form["JOB_NAME"];
            var jobGroup = context.Request.Form["JOB_GROUP"];
            var description = context.Request.Form["DESCRIPTION"] ?? string.Empty;
            var cron = context.Request.Form["CRON_EXPRESSION"];
            var time1 = context.Request.Form["START_TIME"];
            var time2 = context.Request.Form["END_TIME"];
            var host = context.Request.Form["REQUESTHOST"];
            var path = context.Request.Form["REQUESTPATH"];

            if (string.IsNullOrEmpty(cron))
            {
                return;
            }

            var info = new SettingInfo
            {
                CRON_EXPRESSION = cron,
                DESCRIPTION = description,
                END_TIME = time2,
                START_TIME = time1,
                JOB_NAME = jobName,
                JOB_GROUP = jobGroup,
                REQUESTHOST = host,
                REQUESTPATH = path,
                HOST = context.Request.Form["HOST"] ?? string.Empty,
                TRIGGER_GROUP = jobGroup,
                TRIGGER_NAME = "TN_" + DateTime.Now.Ticks,
            };

            DateTime temp;
            if (!DateTime.TryParse(info.START_TIME, out temp))
            {
                info.START_TIME = DateTime.Now.AddYears(-1).ToString();
            }
            if (!DateTime.TryParse(info.END_TIME, out temp))
            {
                info.END_TIME = DateTime.Now.AddYears(100).ToString();
            }

            if (string.IsNullOrEmpty(info.JOB_NAME))
            {
                info.JOB_NAME = string.Format("Job{0}{1}", info.REQUESTPATH.Replace("/", string.Empty).Trim(), DateTime.Now.ToString("fff"));

                DbHelper.InsertSetting(info);
                context.Response.Write(info.JOB_NAME);
            }
            else
            {
                var count = DbHelper.UpdateSetting(info);
                context.Response.Write(count.ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}