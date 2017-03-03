using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AutoTask.GloblaWeb
{
    /// <summary>
    /// logout 的摘要说明
    /// </summary>
    public class logout : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //删除用户票据
            FormsAuthentication.SignOut();

            //重新定向到登陆页面
            FormsAuthentication.RedirectToLoginPage();
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