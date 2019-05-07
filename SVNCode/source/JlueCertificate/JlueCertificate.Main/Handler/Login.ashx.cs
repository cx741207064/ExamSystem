using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace JlueCertificate.Main.Handler
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            string result = string.Empty;
            try
            {
                string _examid = string.Empty;
                string _cardid = string.Empty;
                string _vcode = string.Empty;
                Dictionary<string, string> dicHeaders = HttpContextSecurity.HttpContextHeaderParams(context);
                foreach (var item in dicHeaders)
                {
                    switch (item.Key.ToLower())
                    {
                        case "examid": _examid = item.Value; break;
                        case "cardid": _cardid = item.Value; break;
                        case "vcode": _vcode = item.Value; break;
                        default:
                            break;
                    }
                }
                string _vcodeCookie = HttpContextSecurity.HttpContextCookie(context,Untity.HelperHttp.Cookie_VerfyCode);

                result = Logic.Exam.UserCenter.login(_examid, _cardid, _vcode, _vcodeCookie);

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
                result = "{\"Code\": -1,\"Data\": \"\",\"Msg\": " + ex.Message.ToString() + ",\"Stamp\": \"\"}";
            }
            finally
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = "{\"Code\": -1,\"Data\": \"\",\"Msg\": \"请求过度频繁，请稍后再试!\",\"Stamp\": \"\"}";
                }
                context.Response.Write(result);
                context.Response.Flush();
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