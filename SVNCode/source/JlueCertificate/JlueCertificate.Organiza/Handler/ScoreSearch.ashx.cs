using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace JlueCertificate.Organiza.Handler
{
    /// <summary>
    /// ExamCenter 的摘要说明
    /// </summary>
    public class ScoreSearch : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            string result = string.Empty;
            string action = context.Request["action"].ToString();
            string _uid = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_UserId);
            string _pwd = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_PassId);
            if (!string.IsNullOrEmpty(_uid) && !string.IsNullOrEmpty(_pwd))
            {
                if (!string.IsNullOrEmpty(action) && HttpContextSecurity.HttpContextQuerySafe(context))
                {
                    if (context.Request.HttpMethod.ToUpper() == "GET")
                    {
                        #region Get处理
                        switch (action.ToLower())
                        {
                            case "getscore": result = Logic.Score.ScoreSearch.getscore(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["ticketnum"]), context.Request["page"], context.Request["limit"]); break;
                            case "getscoredetail": result = Logic.Score.ScoreSearch.getscoredetail(HttpContextSecurity.HttpContextParam(context.Request["ticketid"]), HttpContextSecurity.HttpContextParam(context.Request["OLSchoolUserId"])); break;
                            case "getsubjectsbyticket": result = Logic.Score.ScoreSearch.getSubjectsByTicket(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["id"])); break;
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
                if (!string.IsNullOrEmpty(action) && HttpContextSecurity.HttpContextQuerySafe(context))
                {
                    if (context.Request.HttpMethod.ToUpper() == "GET")
                    {
                        #region Get处理
                        switch (action.ToLower())
                        {
                            case "getscoredetail": result = Logic.Score.ScoreSearch.getscoredetail(HttpContextSecurity.HttpContextParam(context.Request["ticketid"]), HttpContextSecurity.HttpContextParam(context.Request["OLSchoolUserId"])); break;
                            case "getstudentsubjectscore": result = Logic.Score.ScoreSearch.getStudentSubjectScore(HttpContextSecurity.HttpContextParam(context.Request["studentid"]), HttpContextSecurity.HttpContextParam(context.Request["aomid"])); break;
                            default:
                                break;
                        }
                        #endregion
                    }
                    if (context.Request.HttpMethod.ToUpper() == "POST")
                    {
                        #region Post处理
                        string postString = HttpContextSecurity.getPostStr(context);
                        switch (action.ToLower())
                        {
                            case "addstudentsubjectscore": result = Logic.Score.ScoreSearch.addStudentSubjectScore(postString); break;
                            case "updatestudentsubjectscore": result = Logic.Score.ScoreSearch.updateStudentSubjectScore(postString); break;
                            case "querenjiaojuan": result = Logic.Score.ScoreSearch.addStudentSubjectScore(postString); break;
                            case "shifoujiaojuan": result = Logic.Score.ScoreSearch.getIsexaminSubjectScore(postString); break;
                            default:
                                break;
                        }
                        #endregion
                    }
                }
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}