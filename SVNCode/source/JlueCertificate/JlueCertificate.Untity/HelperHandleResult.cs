using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Untity
{
    [Serializable]
    public class HelperHandleResult
    {
        public HelperHandleResult()
        {
            Code = "0";
            Msg = "请求成功";
            Stamp = "";
        }
        /// <summary>
        /// 返回的Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 异常描述或信息描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Stamp { get; set; }
    }
}
