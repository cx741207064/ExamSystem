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
        public override void Init()
        {
            EndRequest += (sender, e) => myEndRequest();
            base.Init();
        }

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

        }

        void myEndRequest()
        {
            Exception error = Context.Error;
            if (error != null)
            {
                string result = "{\"Code\": -1,\"Data\": \"\",\"Msg\": \"" + error.Message + "\",\"Stamp\": \"\"}";
                Response.StatusCode = 500;
                Response.ClearContent();
                Response.Write(result);
            }
        }

    }
}
