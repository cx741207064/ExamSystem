using JlueCertificate.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Bll.Organiz
{
    public class ScoreSearch
    {
        public static object getscore(string _uid, string _pwd, string _ticketnum, string page, string limit, ref long count, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            Entity.Respose.getscore result = new Entity.Respose.getscore();
            if (_orga != null)
            {
                result.all = Dal.MsSQL.T_StudentTicket.GetListByTicketNum(_orga.Id, _ticketnum, page, limit, ref count);
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return result;
        }

        public static object getscoredetail(string _ticketid, string _OLSchoolUserId, ref string error)
        {
            Entity.Respose.getscoredetail result = new Entity.Respose.getscoredetail();
            Entity.MsSQL.T_StudentTicket ticketmodel = Dal.MsSQL.T_StudentTicket.GetModel(_ticketid);
            if (ticketmodel != null)
            {
                List<Entity.Respose.allcertifisubject> _certifisubjectlist = Dal.MsSQL.T_CertifiSubject.GetAllListByCertId(ticketmodel.CertificateId);
                Entity.MsSQL.T_Certificate certifimodel = Dal.MsSQL.T_Certificate.GetModel(Untity.HelperDataCvt.strToIni(ticketmodel.CertificateId));
                //获取计算方式
                string _normalaccount = string.Join(" + ", _certifisubjectlist.Select(ii => (ii.Name.ToString() + "*" + ii.NormalResult.ToString() + "%")).ToList());
                string _examaccount = string.Join(" + ", _certifisubjectlist.Select(ii => (ii.Name.ToString() + "*" + ii.ExamResult.ToString() + "%")).ToList());
                string _accountform = "(" + _normalaccount + ") * " + certifimodel.NormalResult + "% + "
                    + "(" + _examaccount + ") * " + certifimodel.ExamResult + "%";
                //获取网校课程得分情况并计算得分情况
                string _subjectids = string.Join(",", _certifisubjectlist.Select(ii => ii.OLSchoolAOMid).ToList());
                List<Entity.Respose.scoredetail> _olscoredetail = Dal.MsSQL.T_StudentTicket.GetScoreDetailFromOLSchool(_OLSchoolUserId, _subjectids);
                //总得分，平时，考试
                double _scoresum = 0;
                double _normalsum = 0;
                double _examsum = 0;
                foreach (Entity.Respose.scoredetail item in _olscoredetail)
                {
                    item.Category = _certifisubjectlist.Where(i => i.OLSchoolAOMid == item.AOMid).FirstOrDefault().Category;
                    item.Name = _certifisubjectlist.Where(i => i.OLSchoolAOMid == item.AOMid).FirstOrDefault().Name;
                    item.NormalResult = _certifisubjectlist.Where(i => i.OLSchoolAOMid == item.AOMid).FirstOrDefault().NormalResult;
                    item.ExamResult = _certifisubjectlist.Where(i => i.OLSchoolAOMid == item.AOMid).FirstOrDefault().ExamResult;
                    _normalsum += Untity.HelperDataCvt.strToDouble(item.NormalScore) * Untity.HelperDataCvt.strToDouble(item.NormalResult) / 100;
                    _examsum += Untity.HelperDataCvt.strToDouble(item.ExamScore) * Untity.HelperDataCvt.strToDouble(item.ExamResult) / 100;
                }
                _scoresum = (_normalsum * Untity.HelperDataCvt.strToDouble(certifimodel.NormalResult) / 100 +
                    _examsum * Untity.HelperDataCvt.strToDouble(certifimodel.ExamResult) / 100);

                result.all = _olscoredetail;
                result.scoresum = Math.Round(_scoresum, 2).ToString();
                result.accountform = _accountform;
            }
            else
            {
                error = "证书有异常，无法查询";
                return "-1";
            }
            return result;
        }

        public static object getSubjectsByTicket(string _uid, string _pwd, string id, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            var result = new object();
            if (_orga != null)
            {
                result = Dal.MsSQL.T_StudentTicket.getSubjectsByTicket(id);
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return result;
        }

    }
}
