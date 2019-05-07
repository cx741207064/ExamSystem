using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperAppSet
    {
        #region 公共方法
        /// <summary>
        /// 获取WebConfig.AppSetting内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getAppSetting(string key)
        {
            string result = "";
            try
            {
                result = ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
            }
            return result ?? "";
        }
        #endregion
    }
}
