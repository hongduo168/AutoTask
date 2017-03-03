using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace AutoTask.Web
{
    /// <summary>
    /// service_manager 的摘要说明
    /// </summary>
    public class service_restart : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var serviceName = System.Configuration.ConfigurationManager.AppSettings["ServiceName"].ToString();

            using (ServiceController sc = new ServiceController(serviceName))
            {
                if (sc != null)
                {
                    //停止服务
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                    }

                    //启动服务
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running);
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write("ok");
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