using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Base
{
    public class BaseData
    {
        public static string getCitys()
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Base.BaseData.getCitys();
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            finally
            {
                if (!string.IsNullOrEmpty(error))
                {
                    result.Code = "-1";
                    result.Msg = error;
                }
            }

            return Untity.HelperJson.SerializeObject(result);
        }

        public static string getOLSchoolSubjects(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Base.BaseData.getOLSchoolSubjects(_uid, _pwd, ref error);
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
            }
            finally
            {
                if (!string.IsNullOrEmpty(error))
                {
                    result.Code = "-1";
                    result.Msg = error;
                }
            }
            return Untity.HelperJson.SerializeObject(result);
        }
    }
}
