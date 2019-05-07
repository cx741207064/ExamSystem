using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using JlueCertificate.Organiza;

namespace JlueCertificate.Organiza
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

            Exception erroy = Server.GetLastError();
            string err = "出错页面是：" + Request.Url.ToString() + "<br>";
            err += "异常信息：" + erroy.Message + "<br>";
            err += "Source:" + erroy.Source + "<br>";
            err += "StackTrace:" + erroy.StackTrace + "<br>";
            Server.ClearError();

        }
    }
}
