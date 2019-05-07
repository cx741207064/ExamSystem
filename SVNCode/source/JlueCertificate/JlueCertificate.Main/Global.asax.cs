using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace JlueCertificate.Main
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

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception erroy = Server.GetLastError();
            string err = "出错页面是：" + Request.Url.ToString() + "<br>";
            err += "异常信息：" + erroy.Message + "<br>";
            err += "Source:" + erroy.Source + "<br>";
            err += "StackTrace:" + erroy.StackTrace + "<br>";
            Server.ClearError();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}