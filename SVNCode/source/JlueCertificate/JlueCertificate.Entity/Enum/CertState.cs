using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Enum
{
    public class CertState
    {
        /// <summary>
        /// 0
        /// </summary>
        public static string 未报名 = "0";
        /// <summary>
        /// 1
        /// </summary>
        public static string 已报名 = "1";
        /// <summary>
        /// 2
        /// </summary>
        public static string 已发证 = "2";
    }

    /// <summary>
    /// 目前已报名证书考试状态
    /// </summary>
    public class TicketState
    {
        /// <summary>
        /// 未考试
        /// </summary>
        public static string 未考试 = "1";
        /// <summary>
        /// 已考试
        /// </summary>
        public static string 已考试 = "2";
    }
}
