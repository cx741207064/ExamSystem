using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace JlueCertificate.Main.Handler
{
    public class HttpContextSecurity
    {
        public static bool HttpContextQuerySafe(HttpContext context)
        {
            string getkeys = "";
            bool Safe = true;
            if (context.Request.QueryString != null)
            {

                for (int i = 0; i < context.Request.QueryString.Count; i++)
                {
                    getkeys = context.Request.QueryString.Keys[i];
                    if (!ProcessSqlStr(context.Request.QueryString[getkeys]))
                    {
                        Safe = false;
                    }
                }
            }
            try
            {
                if (context.Request.Form != null)
                {
                    for (int i = 0; i < context.Request.Form.Count; i++)
                    {
                        getkeys = context.Request.Form.Keys[i];
                        if (!ProcessSqlStr(context.Request.Form[getkeys]))
                        {
                            Safe = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return Safe;
        }

        private static bool ProcessSqlStr(string inputString)
        {
            string SqlStr = @"and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";
            try
            {
                if ((inputString != null) && (inputString != String.Empty))
                {
                    string str_Regex = @"\b(" + SqlStr + @")\b";
                    Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
                    if (true == Regex.IsMatch(inputString))
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static Dictionary<string, string> HttpContextHeaderParams(HttpContext context)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                string _authorization = context.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(_authorization))
                {
                    _authorization = HttpContextParam(_authorization);
                    foreach (var item in _authorization.Split('&'))
                    {
                        if (item.IndexOf('=') > -1)
                        {
                            string _key = item.Substring(0, item.IndexOf('='));
                            string _value = item.Substring(item.IndexOf('=') + 1);
                            if (!string.IsNullOrEmpty(_key) && !string.IsNullOrEmpty(_value))
                            {
                                result.Add(_key, _value);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static string HttpContextCookie(HttpContext context,string key)
        {
            string result = string.Empty;
            try
            {
                var _cookies = context.Request.Cookies;
                foreach (var item in _cookies)
                {
                    if (item.ToString() == key)
                    {
                        string _valueStr = HttpContextParam(_cookies[item.ToString()].Value);
                        if (!string.IsNullOrEmpty(_valueStr))
                        {
                            result = _valueStr;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static bool HttpContextRefreshSafe(HttpContext context)
        {
            bool result = true;
            try
            {
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 获取请求IP
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string getIp(HttpContext context)
        {
            string result = "127.0.0.1";
            try
            {
                HttpRequest Request = HttpContext.Current.Request;
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    result = Request.ServerVariables["REMOTE_ADDR"];
                else
                    result = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result == null || result == "")
                    result = Request.UserHostAddress;
                if (result == "::1")
                    result = "127.0.0.1";
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 解析PostData
        /// </summary>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string getPostStr(HttpContext context)
        {
            string result = string.Empty;
            try
            {
                using (Stream stream = context.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    result = HttpContextParam(Encoding.UTF8.GetString(postBytes));
                }

            }
            catch (Exception ex)
            {
            }
            return result;
        }

        internal static string HttpContextParam(string _param)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(_param))
                {
                    _param = HttpUtility.UrlDecode(_param, System.Text.Encoding.UTF8);
                    _param = HttpUtility.UrlDecode(_param, System.Text.Encoding.UTF8);
                    result = HttpUtility.UrlDecode(_param, System.Text.Encoding.UTF8);
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

    }
}