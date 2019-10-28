using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace JlueCertificate.Organiza.Handler
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class WXLogin : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string res = String.Empty;
            try
            {
                string classid = context.Request["classid"] ?? "";
                string signature = context.Request["signature"] ?? "";
                string timestamp = context.Request["timestamp"] ?? "";
                string nonce = context.Request["nonce"] ?? "";

                //bool isacce = Untity.TokenHelper.TokenVerify(signature, Convert.ToDateTime(timestamp), nonce);
                //if (isacce)
                //{
                result = Logic.Organiz.UserCenter.wxlogin(classid);
                //}

                #region GZIP

                string _acceptEncoing = context.Request.Headers["Accept-Encoding"];
                if (!string.IsNullOrEmpty(_acceptEncoing))
                {
                    if (_acceptEncoing.ToLower().Contains("deflate"))
                    {
                        //向输出流头部添加Deflate压缩信息
                        context.Response.AppendHeader("Content-encoding", "deflate");
                        context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                    }
                    else
                    {
                        if (_acceptEncoing.ToLower().Contains("gzip"))
                        {
                            //向输出流头部添加GZIP压缩信息
                            context.Response.AppendHeader("Content-encoding", "gzip");
                            context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = ex.Message.ToString();
            }
            finally
            {
                if (result.Code == "-1")
                {
                    context.Response.Write(Untity.HelperJson.SerializeObject(result));
                    context.Response.Flush();
                }
                else
                {
                    context.Response.Redirect("/Pages/index.html");
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