using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JlueCertificate.Untity
{
    public class HelperFile
    {
        public static string checkFileImage(string fileType, int fileLength)
        {
            string result = string.Empty;
            string fileTypes = "gif,jpg,jpeg,png,bmp";
            try
            {
                if (fileType.Contains('.') && fileTypes.IndexOf(fileType.Split('.')[1]) > -1)
                {
                    result = string.Format("{0}.{1}", Guid.NewGuid(), fileType.Split('.')[1]);
                }
                if (fileType.Contains('/') && fileTypes.IndexOf(fileType.Split('/')[1]) > -1)
                {
                    result = string.Format("{0}.{1}", Guid.NewGuid(), fileType.Split('/')[1]);
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static bool saveFile(string fileName, string content)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                File.WriteAllText(fileName, content);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool deleteFile(string fileName)
        {
            bool result = false;
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                if (!File.Exists(fileName))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 创建文件目录
        /// </summary>
        /// <param name="dirTempPath"></param>
        public static void createFileDirectory(string dirTempPath)
        {
            if (!Directory.Exists(dirTempPath))
            {
                Directory.CreateDirectory(dirTempPath);
            }
        }
    }
}
