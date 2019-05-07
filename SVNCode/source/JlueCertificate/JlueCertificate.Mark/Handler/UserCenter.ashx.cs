using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace JlueCertificate.Mark.Handler
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
                string _uid = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_MarkUserId);
                string _pwd = HttpContextSecurity.HttpContextCookie(context, Untity.HelperHttp.Cookie_MarkPassId);
                if (!string.IsNullOrEmpty(_uid) && !string.IsNullOrEmpty(_pwd))
                {
                    if (!string.IsNullOrEmpty(action) && HttpContextSecurity.HttpContextQuerySafe(context))
                    {

                        if (context.Request.HttpMethod.ToUpper() == "GET")
                        {
                            #region Get处理
                            switch (action.ToLower())
                            {
                                case "userinfo": result = Logic.Mark.UserCenter.userinfo(_uid, _pwd); break;
                                case "getuser": result = Logic.Mark.UserCenter.getuser(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    context.Request["page"], context.Request["limit"]); break;
                                case "getorganiza": result = Logic.Mark.UserCenter.getorganiza(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    context.Request["page"], context.Request["limit"]); break;
                                case "hangye": result = Logic.Mark.UserCenter.hangye(_uid, _pwd); break;
                                case "hangyestudent": result = Logic.Mark.UserCenter.hangyestudent(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["hangye"]),
                                    context.Request["page"], context.Request["limit"]); break;
                                case "getallsubject": result = Logic.Mark.UserCenter.getallsubject(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    context.Request["page"], context.Request["limit"]); break;
                                case "getsubjectsbycertid": result = Logic.Mark.UserCenter.getsubjectsbycertid(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["certid"]),
                                    context.Request["page"], context.Request["limit"]); break;
                                case "getallsubjectnopage": result = Logic.Mark.UserCenter.getallsubjectnopage(_uid, _pwd); break;
                                case "getolschoolsubjects": result = Logic.Base.BaseData.getOLSchoolSubjects(_uid, _pwd); break;
                                case "getallcertificate": result = Logic.Mark.UserCenter.getallcertificate(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    context.Request["page"], context.Request["limit"]); break;
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
                                case "adduser": result = Logic.Mark.UserCenter.adduser(_uid, _pwd, postString); break;
                                case "updateuser": result = Logic.Mark.UserCenter.updateuser(_uid, _pwd, postString); break;
                                case "updateuserpwd": result = Logic.Mark.UserCenter.updateuserpwd(_uid, _pwd, postString); break;
                                case "deleteuser": result = Logic.Mark.UserCenter.deleteuser(_uid, _pwd, postString); break;
                                case "addorganiza": result = Logic.Mark.UserCenter.addorganiza(_uid, _pwd, postString); break;
                                case "updateorganiza": result = Logic.Mark.UserCenter.updateorganiza(_uid, _pwd, postString); break;
                                case "updateorganizapwd": result = Logic.Mark.UserCenter.updateorganizapwd(_uid, _pwd, postString); break;
                                case "deleteorganiza": result = Logic.Mark.UserCenter.deleteorganiza(_uid, _pwd, postString); break;
                                case "addsubject": result = Logic.Mark.UserCenter.addsubject(_uid, _pwd, context.Server.MapPath("/"), postString); break;
                                case "updatesubject": result = Logic.Mark.UserCenter.updatesubject(_uid, _pwd, context.Server.MapPath("/"), postString); break;
                                case "delsubject": result = Logic.Mark.UserCenter.delsubject(_uid, _pwd, postString); break;
                                case "addcertificate": result = Logic.Mark.UserCenter.addcertificate(_uid, _pwd, postString); break;
                                case "updatecertificate": result = Logic.Mark.UserCenter.updatecertificate(_uid, _pwd, postString); break;
                                case "delcertificate": result = Logic.Mark.UserCenter.delcertificate(_uid, _pwd, postString); break;
                                case "addcertifisubject": result = Logic.Mark.UserCenter.addcertifisubject(_uid, _pwd, postString); break;
                                case "updatecertifisubject": result = Logic.Mark.UserCenter.updatecertifisubject(_uid, _pwd, postString); break;
                                case "delcertifisubject": result = Logic.Mark.UserCenter.delcertifisubject(_uid, _pwd, postString); break;
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