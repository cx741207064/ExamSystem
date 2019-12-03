using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;
using JlueCertificate;

namespace JlueCertificate.Organiza.Handler
{
    /// <summary>
    /// ExamCenter 的摘要说明
    /// </summary>
    public class ExamCenter : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "application/json";
            context.Response.Charset = "utf-8";
            string result = string.Empty;
            try
            {
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
                                //判断准考证是否绑定座位
                                case "isbountseat": result = Logic.Organiz.ExamCenter.isbountseat(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["TicketNum"])); break;
                                //获取座位
                                case "getexamseatinfo": result = Logic.Organiz.ExamCenter.getexamseatInfo(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["ExamRoomId"])); break;
                                //获取考场
                                case "getexaminfo": result = Logic.Organiz.ExamCenter.getexamInfo(_uid, _pwd); break;
                                //查询准考证考场信息
                                case "getticketprintinfo": result = Logic.Organiz.ExamCenter.getticketprintInfo(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["TicketNum"])); break;
                                case "getstudentid": result = Logic.Organiz.ExamCenter.getStudentId(_uid, _pwd); break;
                                case "getstudent": result = Logic.Organiz.ExamCenter.getStudent(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    HttpContextSecurity.HttpContextParam(context.Request["cardid"]), context.Request["page"], context.Request["limit"]); break;
                                case "getunsignupcertificate": result = Logic.Organiz.ExamCenter.getunsignupcertificate(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    HttpContextSecurity.HttpContextParam(context.Request["cardid"]), context.Request["page"], context.Request["limit"]); break;
                                case "getsignupcertificate": result = Logic.Organiz.ExamCenter.getsignupcertificate(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    HttpContextSecurity.HttpContextParam(context.Request["cardid"]), context.Request["page"], context.Request["limit"]); break;
                                case "getholdcertificate": result = Logic.Organiz.ExamCenter.getholdcertificate(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                    HttpContextSecurity.HttpContextParam(context.Request["cardid"]), context.Request["page"], context.Request["limit"]); break;
                                case "getstudentcertifi": result = Logic.Organiz.ExamCenter.getstudentcertifi(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["studentid"])); break;
                                case "getcertifiprint": result = Logic.Organiz.ExamCenter.getcertifiprint(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["SerialNum"])); break;
                                case "getticketprint": result = Logic.Organiz.ExamCenter.getticketprint(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["TicketNum"])); break;
                                case "getexamroom":  result = Logic.Organiz.ExamCenter.getexamroom(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                      context.Request["page"], context.Request["limit"]); break;
                                case "getexamroombyid": result = Logic.Organiz.ExamCenter.getexamroombyid(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["id"])); break;
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
                                //座位添加准考证
                                case "updateexamseat": result = Logic.Organiz.ExamCenter.updateexamseat(_uid, _pwd, postString); break;
                                //考场添加座位
                                case "addexamseat": result = Logic.Organiz.ExamCenter.addexamseat(_uid, _pwd, postString); break;
                                //删除考场
                                case "deleteexamroom": result = Logic.Organiz.ExamCenter.deleteexamroom(_uid, _pwd, postString); break;
                                //修改考场
                                case "updateexamroom": result = Logic.Organiz.ExamCenter.updateexamroom(_uid, _pwd, postString); break;
                                //添加考场
                                case "addexamroom": result = Logic.Organiz.ExamCenter.addexamroom(_uid, _pwd, postString); break;
                                //考场查询
                                //case "getexamroom": result = Logic.Organiz.ExamCenter.getexamroom(_uid, _pwd, HttpContextSecurity.HttpContextParam(context.Request["name"]),
                                //    context.Request["page"], context.Request["limit"]); break;

                                case "addstudent": result = Logic.Organiz.ExamCenter.addstudent(_uid, _pwd, postString); break;
                                case "updatestudent": result = Logic.Organiz.ExamCenter.updatestudent(_uid, _pwd, postString); break;
                                case "deletestudent": result = Logic.Organiz.ExamCenter.deletestudent(_uid, _pwd, postString); break;
                                case "signup": result = Logic.Organiz.ExamCenter.signup(_uid, _pwd, postString); break;
                                case "cancel": result = Logic.Organiz.ExamCenter.cancel(_uid, _pwd, postString); break;
                                case "bangding": result = Logic.Organiz.ExamCenter.bangding(_uid, _pwd, postString); break;
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