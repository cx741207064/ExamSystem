using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic
{
    public class Class1
    {
        public static string Test_Get(string test)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Class1.Test(test,ref error);
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
