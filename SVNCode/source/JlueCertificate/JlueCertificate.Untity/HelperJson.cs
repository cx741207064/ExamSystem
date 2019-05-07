using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperJson
    {
        #region 公共方法

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// json转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string JObjectGet(string value, string propertyName)
        {
            string result = "";
            try
            {
                JObject jObject = JObject.Parse(value);
                result = jObject.GetValue(propertyName).ToString();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        /// <summary>
        /// json转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string JObjectSet(Dictionary<string,string> dic)
        {
            string result = "";
            try
            {
                JObject jObject = new JObject();
                foreach (var item in dic)
                {
                    jObject.Add(item.Key, item.Value);
                }
                if (jObject.Count > 0)
                {
                    result = jObject.ToString();
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
