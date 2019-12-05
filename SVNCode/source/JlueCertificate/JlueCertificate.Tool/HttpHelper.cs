using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JlueCertificate.Tool
{
    public sealed class HttpHelper
    {
        private HttpHelper()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 3);
        }

        private static object locker = new object();

        private static HttpHelper _instance = null;

        public static HttpHelper Singleton
        {
            get
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new HttpHelper();
                    }
                    return _instance;
                }
            }
        }

        private static HttpClient httpClient
        {
            get;
            set;
        }

        public string HttpGet(string requestUri)
        {
            var hrm = httpClient.GetAsync(requestUri);
            var result = hrm.ContinueWith(a =>
                 {
                     string stringResult = null;
                     switch (a.Status)
                     {
                         case TaskStatus.RanToCompletion:
                             stringResult = a.Result.Content.ReadAsStringAsync().Result;
                             break;
                         case TaskStatus.Canceled:
                             stringResult = "请求已取消";
                             break;
                         case TaskStatus.Faulted:
                             stringResult = string.Format("远程服务器发生错误,URI:{0}", requestUri);
                             break;
                     }
                     return stringResult;
                 });
            result.Wait();
            return result.Result;
        }

        public string HttpPost(string requestUri, Dictionary<string, string> dic)
        {
            HttpContent httpContent = new FormUrlEncodedContent(dic);
            var hrm = httpClient.PostAsync(requestUri, httpContent);
            var result = hrm.ContinueWith(a =>
                {
                    string stringResult = null;
                    switch (a.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            stringResult = a.Result.Content.ReadAsStringAsync().Result;
                            break;
                        case TaskStatus.Canceled:
                            stringResult = "请求已取消";
                            break;
                        case TaskStatus.Faulted:
                            stringResult = string.Format("远程服务器发生错误,URI:{0}", requestUri);
                            break;
                    }
                    return stringResult;
                });
            result.Wait();
            return result.Result;
        }

    }
}
