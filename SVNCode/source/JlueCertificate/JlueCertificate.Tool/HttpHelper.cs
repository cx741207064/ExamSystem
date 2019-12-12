using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            //httpClient.Timeout = new TimeSpan(0, 0, 3);
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

        public Task<HttpResponseResult> HttpGet(string requestUri)
        {
            var hrm = httpClient.GetAsync(requestUri);
            var result = hrm.ContinueWith(a =>
                 {
                     HttpResponseResult hrr = new HttpResponseResult();
                     string stringResult = null;
                     string msg = null;
                     switch (a.Status)
                     {
                         case TaskStatus.RanToCompletion:
                             if (a.Result.StatusCode == HttpStatusCode.OK)
                             {
                                 stringResult = a.Result.Content.ReadAsStringAsync().Result;
                                 hrr.isSuccess = true;
                                 hrr.Data = stringResult;
                             }
                             else
                             {
                                 msg = "远程服务器返回不正确";
                             }
                             hrr.Code = (int)a.Result.StatusCode;
                             break;
                         case TaskStatus.Canceled:
                             msg = "请求已取消";
                             break;
                         case TaskStatus.Faulted:
                             msg = "服务器发生错误";
                             break;
                     }
                     hrr.Message = msg;
                     return hrr;
                 });
            //result.Wait();
            return result;
        }

        public Task<HttpResponseResult> HttpPost(string requestUri, Dictionary<string, string> dic = null)
        {
            if (dic == null)
            {
                dic = new Dictionary<string, string>();
            }
            HttpContent httpContent = new FormUrlEncodedContent(dic);
            var hrm = httpClient.PostAsync(requestUri, httpContent);
            var result = hrm.ContinueWith(a =>
                {
                    HttpResponseResult hrr = new HttpResponseResult();
                    string stringResult = null;
                    string msg = null;
                    switch (a.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            if (a.Result.StatusCode == HttpStatusCode.OK)
                            {
                                stringResult = a.Result.Content.ReadAsStringAsync().Result;
                                hrr.isSuccess = true;
                                hrr.Data = stringResult;
                            }
                            else
                            {
                                msg = "远程服务器返回不正确";
                            }
                            hrr.Code = (int)a.Result.StatusCode;
                            break;
                        case TaskStatus.Canceled:
                            msg = "请求已取消";
                            break;
                        case TaskStatus.Faulted:
                            msg = "服务器发生错误";
                            break;
                    }
                    hrr.Message = msg;
                    return hrr;
                });
            //result.Wait();
            return result;
        }

    }
}
