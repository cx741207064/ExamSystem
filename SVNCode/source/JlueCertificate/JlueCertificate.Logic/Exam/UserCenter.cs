using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Exam
{
    public class UserCenter
    {

        public static string login(string _examid, string _cardid, string _vcode, string _vcodeCookie)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Exam.UserCenter.login(_examid, _cardid, _vcode, _vcodeCookie, ref error);
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
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_ExamId, _examid, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_CardId, _cardid, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_VerfyCode, _cardid, -1);
                }
                else
                {
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_ExamId, _examid, 60 * 12);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_CardId, _cardid, 60 * 12);
                }
            }

            return Untity.HelperJson.SerializeObject(result);
        }

        public static string userinfo(string _examid, string _cardid)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                //result.Data = Bll.Exam.UserCenter.userinfo(_examid, _cardid, ref error);
                result.Data = Bll.Exam.UserCenter.userinfo(_examid, out error);
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


        public static string examrefresh(string _examid, string _cardid, string p)
        {
            throw new NotImplementedException();
        }

        public static string subjectinfo(string _examid, string _cardid, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Exam.UserCenter.subjectinfo(_examid, _cardid,postString, ref error);
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
