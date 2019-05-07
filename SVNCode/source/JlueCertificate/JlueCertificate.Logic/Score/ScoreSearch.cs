using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Score
{
    public class ScoreSearch
    {
        public static string getscore(string _uid, string _pwd, string _ticketnum, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ScoreSearch.getscore(_uid, _pwd, _ticketnum, page, limit, ref count, ref error);
                result.Msg = error;
                result.Stamp = count.ToString();
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                result.Code = "-1";
                result.Msg = error;
            }
            finally
            {
            }
            return Untity.HelperJson.SerializeObject(result);
        }

        public static string getscoredetail(string _ticketid, string _OLSchoolUserId)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ScoreSearch.getscoredetail(_ticketid, _OLSchoolUserId, ref error);
                result.Msg = error;
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                result.Code = "-1";
                result.Msg = error;
            }
            finally
            {
            }
            return Untity.HelperJson.SerializeObject(result);
        }
    }
}
