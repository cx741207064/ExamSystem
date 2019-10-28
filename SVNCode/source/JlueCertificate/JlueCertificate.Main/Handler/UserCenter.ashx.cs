using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace JlueCertificate.Main.Handler
{
    /// <summary>
    /// UserCenter 的摘要说明
    /// </summary>
    public class UserCenter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            string result = string.Empty;
            try
            {
                string action = context.Request["action"].ToString();
                string _examid = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_ExamId);
                string _cardid = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_CardId);
                if (!string.IsNullOrEmpty(_examid) && !string.IsNullOrEmpty(_cardid))
                {
                    if (!string.IsNullOrEmpty(action) && HttpContextSecurity.HttpContextQuerySafe(context))
                    {

                        if (context.Request.HttpMethod.ToUpper() == "GET")
                        {
                            #region Get处理
                            switch (action.ToLower())
                            {
                                case "userinfo": result = Logic.Exam.UserCenter.userinfo(_examid, _cardid); break;
                                case "examrefresh": result = Logic.Exam.UserCenter.examrefresh(_examid, _cardid, context.Request["subjectid"]); break;
                                default:
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Post处理
                            string postString = HttpContextSecurity.getPostStr(context);
                            switch (action.ToLower())
                            {
                                case "subjectinfo": result = Logic.Exam.UserCenter.subjectinfo(_examid, _cardid, postString); break;
                                case "updatestateto2": result = Logic.Exam.UserCenter.updatestateto2(_examid); break;
                                default:
                                    break;
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    result = "{\"Code\": -10,\"Data\": \"\",\"Msg\": \"登录失效，请重新登陆\",\"Stamp\": \"\"}";
                }
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