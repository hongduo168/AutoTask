using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AutoTask.GloblaWeb
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";

            var passcode = context.Request.Form["passcode"];
            if (!string.IsNullOrEmpty(passcode) && passcode.Equals(System.Configuration.ConfigurationManager.AppSettings["AuthenticatWord"], StringComparison.CurrentCulture))
            {
                //建立表单验证票据
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, passcode, DateTime.Now, DateTime.Now.AddMinutes(300), false, "", "/");

                //使用webcongfi中定义的方式,加密序列化票据为字符串
                string hashTicket = FormsAuthentication.Encrypt(ticket);

                //将加密后的票据转化成cookie
                HttpCookie userCookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);

                //添加到客户端cookie
                context.Response.Cookies.Add(userCookie);

                context.Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                FormsAuthentication.RedirectToLoginPage();
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