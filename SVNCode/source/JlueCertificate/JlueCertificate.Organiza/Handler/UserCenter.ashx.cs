﻿using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace JlueCertificate.Organiza.Handler
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
                                case "userinfo": result = Logic.Organiz.UserCenter.userinfo(_uid, _pwd); break;
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