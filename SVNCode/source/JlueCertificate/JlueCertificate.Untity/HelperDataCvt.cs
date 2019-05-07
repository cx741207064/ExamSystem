using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperDataCvt
    {
        #region 公共方法

        /// <summary>
        /// 对象转字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string objToString(object obj)
        {
            string result = "";
            try
            {
                result = obj == null ? "" : obj.ToString();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 字符串转时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime strToDatetime(string str)
        {
            DateTime result = DateTime.Parse("1900-01-01 00:00:00");
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    result = DateTime.Parse(str);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 字符串转长整形
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long strToLong(string str, long defaultValue = -1)
        {
            long result = defaultValue;
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    result = long.Parse(str);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// 字符串转整形
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int strToIni(string str, int defaultValue = -1)
        {
            int result = defaultValue;
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    result = int.Parse(str);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static double strToDouble(string str, double defaultValue = -1)
        {
            double result = defaultValue;
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    result = double.Parse(str);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int DateTimeToUnix(DateTime dateTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(dateTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 时间转换成Str
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string DateTimeToStr(DateTime dateTime)
        {
            string result = "";
            try
            {
                if (dateTime != null && dateTime > DateTime.Parse("2017-10-01"))
                {
                    result = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public static string DateTimeToStrNoHour(DateTime dateTime)
        {
            string result = "";
            try
            {
                if (dateTime != null && dateTime > DateTime.Parse("2017-10-01"))
                {
                    result = dateTime.ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        #endregion
    }
}
