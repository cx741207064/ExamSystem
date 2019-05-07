using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JlueCertificate.Untity
{
    public class HelperHttp
    {
        public static string Cookie_ExamId = "75D7E5E68D37A9D970C30CBFF9BBEB76";

        public static string Cookie_CardId = "9CBADF809B610EEF45748B4DB31D68D1";

        public static string Cookie_VerfyCode = "E3E264BF9686F235197BEE53BC3A4AE0";

        public static string UrlHost
        {
            get
            {
                return HttpContext.Current.Request.Url.Host;
            }
        }

        public static string DomainUrl
        {
            get
            {
                string domain = UrlHost.Trim().ToLower();
                string rootDomain = ".com.cn|.gov.cn|.cn|.com|.net|.org|.so|.co|.mobi|.tel|.biz|.info|.name|.me|.cc|.tv|.asiz|.hk";
                if (domain.StartsWith("http://")) domain = domain.Replace("http://", "");
                if (domain.StartsWith("https://")) domain = domain.Replace("https://", "");
                if (domain.StartsWith("www.")) domain = domain.Replace("www.", "");
                if (domain.IndexOf("/") > 0)
                    domain = domain.Substring(0, domain.IndexOf("/"));

                foreach (string item in rootDomain.Split('|'))
                {
                    if (domain.EndsWith(item))
                    {
                        domain = domain.Replace(item, "");
                        if (domain.LastIndexOf(".") > 0)
                        {
                            domain = domain.Replace(domain.Substring(0, domain.LastIndexOf(".") + 1), "");
                        }
                        return domain + item;
                    }
                    continue;
                }
                return "";
            }
        }

        public static void InsertCookie(string CookieKey, string CookieValue, int ExpiresMinute)
        {
            HttpCookie cookie = new HttpCookie(CookieKey, HttpUtility.UrlEncode(CookieValue, Encoding.UTF8));
            cookie.Path = "/";
            cookie.Domain = DomainUrl;
            cookie.Expires = DateTime.Now.AddMinutes(ExpiresMinute);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string HttpPost(string url, string body, Encoding dataEncode, string contentType = "application/x-www-form-urlencoded")
        {
            var result = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return result;
                }
                var mRequest = (HttpWebRequest)WebRequest.Create(url);
                //相应请求的参数
                //HttpUtility.UrlEncode(body)
                var data = dataEncode.GetBytes(body);
                mRequest.Method = "Post";
                mRequest.ContentType = contentType;
                mRequest.ContentLength = data.Length;
                mRequest.Timeout = 5000;
                //请求流
                var requestStream = mRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
                //响应流
                var mResponse = (HttpWebResponse)mRequest.GetResponse();
                var responseStream = mResponse.GetResponseStream();
                if (responseStream != null)
                {
                    var streamReader = new StreamReader(responseStream, dataEncode);
                    //获取返回的信息
                    result = streamReader.ReadToEnd();
                    streamReader.Close();
                    responseStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public static string Cookie_UserId = "95633772267E796366D03622ACE232F4";

        public static string Cookie_PassId = "2E04204E5CB2758C0A5B5F3FCAFE1286";

        public static string Cookie_VerfyCodeOrga = "151834974E92D7B94434AE2DCC50F096";

        public static string Cookie_MarkUserId = "E3CEEA3AC665C2E56FDD3DE4DABE4F6B";

        public static string Cookie_MarkPassId = "58C8B8B9C2BF978DA127AEFF8D86C33F";

        public static string Cookie_MarkVerfyCodeOrga = "E53F3F7C85345A629E610268C66EABC4";
    }
}
