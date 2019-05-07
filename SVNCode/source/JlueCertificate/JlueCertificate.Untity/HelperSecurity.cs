using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperSecurity
    {

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(string input, string key = "Q56GtyNkop97H334TtyturfgErvvv98")
        {
            string result = string.Empty;
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input + key));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            result = sBuilder.ToString();

            return result;
        }

    }
}
