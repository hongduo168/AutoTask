using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AutoTask.GloblaWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx.User != null)
            {
                if (ctx.User.Identity.IsAuthenticated == true) //认证成功的用户，才进行授权处理
                {
                    FormsIdentity identity = (FormsIdentity)ctx.User.Identity;
                    string[] roles = identity.Ticket.UserData.Split(','); //将角色字符串,即login.aspx.cs中的“管理员,会员”,变成数组
                    ctx.User = new System.Security.Principal.GenericPrincipal(identity, roles); //将带有角色的信息，重新生成一个GenericPrincipal赋值给User，相当于winform中的Thread.CurrentPrincipal = _principal
                }
            }
            //else
            //{
            //    if (ctx.Request.RawUrl.IndexOf("login.ashx") < 0)
            //    {
            //        ctx.Response.Redirect(FormsAuthentication.LoginUrl);
            //        ctx.Response.End();
            //    }
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}