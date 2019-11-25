using JlueCertificate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JlueCertificate.Mark.Handler
{
    /// <summary>
    /// CertificateCenter 的摘要说明
    /// </summary>
    public class CertificateCenter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];
            string studentId = context.Request["studentId"];
            string certificateId = context.Request["certificateId"];
            string result = string.Empty;
            switch (action.ToLower())
            {
                case "issuecertificate":
                    result = OrganizaRepository.Singleton.IssueCertificate(studentId, certificateId);
                    break;
            }
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            context.Response.Write(result);

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