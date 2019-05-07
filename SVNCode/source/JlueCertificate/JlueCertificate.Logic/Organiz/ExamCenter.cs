﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Logic.Organiz
{
    public class ExamCenter
    {
        public static string getStudent(string _uid, string _pwd, string _name, string _cardid, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            long count = 0;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getStudent(_uid, _pwd, _name, _cardid, page, limit, ref count, ref error);
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

        public static string getStudentId(string _uid, string _pwd)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getStudentId(_uid, _pwd);
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

        public static string addstudent(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.addstudent(_uid, _pwd, postString, ref error);
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

        public static string updatestudent(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.updatestudent(_uid, _pwd, postString, ref error);
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

        public static string deletestudent(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.deletestudent(_uid, _pwd, postString, ref error);
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

        public static string getunsignupcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getunsignupcertificate(_uid, _pwd, _name, _cardid, page, limit, ref count, ref error);
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

        public static string getsignupcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getsignupcertificate(_uid, _pwd, _name, _cardid, page, limit, ref count, ref error);
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

        public static string getholdcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            long count = 0;
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getholdcertificate(_uid, _pwd, _name, _cardid, page, limit, ref count, ref error);
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

        public static string signup(string _uid, string _pwd, string postString)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.signup(_uid, _pwd, postString, ref error);
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

        public static string getstudentcertifi(string _uid, string _pwd, string studentid)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getstudentcertifi(_uid, _pwd, studentid, ref error);
                result.Msg = error;
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = ex.Message.ToString();
            }
            finally
            {
            }
            return Untity.HelperJson.SerializeObject(result);
        }

        public static string getcertifiprint(string _uid, string _pwd, string _serialnum)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getcertifiprint(_uid, _pwd, _serialnum, ref error);
                result.Msg = error;
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = ex.Message.ToString();
            }
            finally
            {
            }
            return Untity.HelperJson.SerializeObject(result);
        }

        public static string getticketprint(string _uid, string _pwd, string _ticketnum)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();
            string error = string.Empty;
            try
            {
                result.Data = Bll.Organiz.ExamCenter.getticketprint(_uid, _pwd, _ticketnum, ref error);
                result.Msg = error;
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = ex.Message.ToString();
            }
            finally
            {
            }
            return Untity.HelperJson.SerializeObject(result);
        }
    }
}
