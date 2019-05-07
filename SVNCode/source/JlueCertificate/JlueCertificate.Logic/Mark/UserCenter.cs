using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Mark
{
    public class UserCenter
    {
        public static string login(string _uid, string _pwd, string _vcode, string _vcodeCookie)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.login(_uid, _pwd, _vcode, _vcodeCookie, ref error);
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
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_MarkUserId, _uid, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_MarkPassId, _pwd, -1);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_MarkVerfyCodeOrga, _pwd, -1);
                }
                else
                {
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_MarkUserId, _uid, 60 * 12);
                    Untity.HelperHttp.InsertCookie(Untity.HelperHttp.Cookie_MarkPassId, _pwd, 60 * 12);
                }
            }

            return Untity.HelperJson.SerializeObject(result);
        }

        public static string userinfo(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.userinfo(_uid, _pwd, ref error);
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

        public static string getuser(string _uid, string _pwd, string _name, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            long count = 0;
            try
            {
                result.Data = Bll.Mark.UserCenter.getuser(_uid, _pwd, _name, page, limit, ref count, ref error);
                result.Stamp = count.ToString();
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

        public static string adduser(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.addstudent(_uid, _pwd, postString, ref error);
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

        public static string updateuser(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updateuser(_uid, _pwd, postString, ref error);
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

        public static string updateuserpwd(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updateuserpwd(_uid, _pwd, postString, ref error);
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

        public static string deleteuser(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.deletestudent(_uid, _pwd, postString, ref error);
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

        public static string getorganiza(string _uid, string _pwd, string _name, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            long count = 0;
            try
            {
                result.Data = Bll.Mark.UserCenter.getorganiza(_uid, _pwd, _name, page, limit, ref count, ref error);
                result.Stamp = count.ToString();
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

        public static string addorganiza(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.addorganiza(_uid, _pwd, postString, ref error);
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

        public static string updateorganiza(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updateorganiza(_uid, _pwd, postString, ref error);
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

        public static string updateorganizapwd(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updateorganizapwd(_uid, _pwd, postString, ref error);
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

        public static string deleteorganiza(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.deleteorganiza(_uid, _pwd, postString, ref error);
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

        public static string hangye(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.hangye(_uid, _pwd, ref error);
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

        public static string hangyestudent(string _uid, string _pwd, string _hangye, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            long count = 0;
            try
            {
                result.Data = Bll.Mark.UserCenter.hangyestudent(_uid, _pwd, _hangye, page, limit, ref count, ref error);
                result.Stamp = count.ToString();
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

        public static string getallsubject(string _uid, string _pwd, string _name, string _page, string _limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.getallsubject(_uid, _pwd, _name, _page, _limit, ref count, ref error);
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

        public static string getsubjectsbycertid(string _uid, string _pwd, string _certid, string _page, string _limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.getsubjectsbycertid(_uid, _pwd, _certid, _page, _limit, ref count, ref error);
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

        public static string addsubject(string _uid, string _pwd, string _path, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.addsubject(_uid, _pwd, _path, postString, ref error);
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

        public static string updatesubject(string _uid, string _pwd, string _path, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updatesubject(_uid, _pwd, _path, postString, ref error);
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

        public static string delsubject(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.delsubject(_uid, _pwd, postString, ref error);
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

        public static string addcertifisubject(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.addcertifisubject(_uid, _pwd, postString, ref error);
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

        public static string updatecertifisubject(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updatecertifisubject(_uid, _pwd, postString, ref error);
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

        public static string delcertifisubject(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.delcertifisubject(_uid, _pwd, postString, ref error);
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

        public static string getallcertificate(string _uid, string _pwd, string _name, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getallcertificate(_uid, _pwd, _name, page, limit, ref count, ref error);
                result.Msg = error;
                result.Stamp = count.ToString();
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

        public static string getallsubjectnopage(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.getallsubjectnopage(_uid, _pwd, ref error);
                result.Msg = error;
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


        public static string addcertificate(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.addcertificate(_uid, _pwd, postString, ref error);
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

        public static string updatecertificate(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.updatecertificate(_uid, _pwd, postString, ref error);
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

        public static string delcertificate(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Mark.UserCenter.delcertificate(_uid, _pwd, postString, ref error);
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
