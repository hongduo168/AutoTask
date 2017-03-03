
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoTask.GloblaWeb
{
    /// <summary>
    /// restart_service 的摘要说明
    /// </summary>
    public class restart_service : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Response.ContentType = "text/plain";
                var jobName = context.Request.Form["JOB_NAME"];
                var jobGroup = context.Request.Form["JOB_GROUP"];
                if (!string.IsNullOrEmpty(jobGroup) && !string.IsNullOrEmpty(jobName))
                {
                    var info = Helper.DbHelper.GetSetting(jobName, jobGroup);
                    if (info != null)
                    {
                        string url = string.Format("{0}/service_restart.ashx", info.HOST.TrimEnd('/'));
                        var result = RequestHelper.GET(url);

                        context.Response.Write(result);
                    }
                }
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