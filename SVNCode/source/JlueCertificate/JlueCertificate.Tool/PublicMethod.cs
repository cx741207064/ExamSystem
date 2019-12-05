using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JlueCertificate.Tool
{
    public class PublicMethod
    {
        public static string DesEncrypt(string StringToEncrypt, byte[] Key, byte[] IV)
        {
            byte[] inputByteArray = Encoding.UTF8.GetBytes(StringToEncrypt);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string DesDecrypt(string EncryptedString, byte[] Key, byte[] IV)
        {
            byte[] inputByteArray = Convert.FromBase64String(EncryptedString);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            cStream.Close();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }

    }
}
