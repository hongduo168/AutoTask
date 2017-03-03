
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceStack.Text;

namespace AutoTask.GloblaWeb
{
    /// <summary>
    /// scheduler_list 的摘要说明
    /// </summary>
    public class scheduler_list : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Response.ContentType = "application/json";
                var list = Helper.DbHelper.GetSettingAll();

                var data = new { list = list, group = list.GroupBy(t => t.JOB_GROUP).Select(t => t.Key) };
                //Dictionary<string, object> data = new Dictionary<string, object>();
                //data["list"] = list;
                //data["group"] = list.GroupBy(t => t.JOB_GROUP).Select(t => t.Key);

                //context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                ServiceStack.Text.JsConfig.IncludeNullValues = true;
                context.Response.Write(data.ToJson());
            }
            else
            {
                context.Response.Write("null");
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