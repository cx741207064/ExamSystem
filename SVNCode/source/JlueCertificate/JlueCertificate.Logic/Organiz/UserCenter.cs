using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Organiz
{
    public class UserCenter
    {
        public static string login(string _uid, string _pwd, string _vcode, string _vcodeCookie)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.UserCenter.login(_uid, _pwd, _vcode, _vcodeCookie, ref error);
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
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_UserId, _uid, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_PassId, _pwd, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_VerfyCodeOrga, _pwd, -1);
                }
                else
                {
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_UserId, _uid, 60 * 12);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_PassId, _pwd, 60 * 12);
                }
            }

            return Untity.HelperJson.SerializeObject(result);
        }

        public static Untity.HelperHandleResult wxlogin(string classid)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            string _uid = string.Empty;
            string _pwd = string.Empty;
            try
            {
                result.Data = Bll.Organiz.UserCenter.wxlogin(classid, ref _uid, ref _pwd, ref error);
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
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_UserId, "error", -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_PassId, "error", -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_VerfyCodeOrga, "error", -1);
                }
                else
                {
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_UserId, _uid, 60 * 12);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_PassId, _pwd, 60 * 12);
                }
            }

            return result;
        }

        public static string userinfo(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.UserCenter.userinfo(_uid, _pwd, ref error);
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
