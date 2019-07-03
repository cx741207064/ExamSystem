using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JlueCertificate.Bll.Mark
{
    public class UserCenter
    {
        public static object login(string _uid, string _pwd, string _vcode, string _vcodeCookie, ref string error)
        {
            if (string.IsNullOrEmpty(_vcodeCookie))
            {
                error = "验证码已失效";
            }
            else if (_vcodeCookie != Untity.HelperSecurity.MD5(_vcode.ToLower()))
            {
                error = "验证码错误";
            }
            else
            {
                Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
                if (_user == null)
                {
                    error = "账号或者密码错误，请重新输入";
                }
            }
            return 1;
        }

        public static object userinfo(string _uid, string _pwd, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user == null)
            {
                error = "账号失效，请重新登陆";
            }
            return _user;
        }

        public static object getuser(string _uid, string _pwd, string _name, string page, string limit, ref long count, ref string error)
        {
            List<Entity.Respose.getmarkuser> result = ConvertMarkUserToResponse(Dal.MsSQL.T_MarkUser.GetListByPage(_name, page, limit, ref count));
            return result;
        }

        private static List<Entity.Respose.getmarkuser> ConvertMarkUserToResponse(List<Entity.MsSQL.T_MarkUser> list)
        {
            List<Entity.Respose.getmarkuser> result = new List<Entity.Respose.getmarkuser>();
            list.ForEach(ii =>
            {
                result.Add(new Entity.Respose.getmarkuser()
                {
                    id = ii.Id.ToString(),
                    name = ii.Name,
                    level = ii.Level == "9" ? "管理员" : "阅卷教师",
                    pwd = ii.Password,
                    createtime = ii.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                });
            });
            return result;
        }


        public static object addstudent(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addmarkuser _markuser = Untity.HelperJson.DeserializeObject<Entity.Request.addmarkuser>(postString);
                Entity.MsSQL.T_MarkUser _model = new Entity.MsSQL.T_MarkUser()
                {
                    Name = Untity.HelperDataCvt.objToString(_markuser.name),
                    Level = Untity.HelperDataCvt.objToString(_markuser.level),
                    Password = Untity.HelperDataCvt.objToString(_markuser.pwd),
                };
                object obj = Dal.MsSQL.T_MarkUser.Add(_model).ToString();
                _markuser.id = obj.ToString();
                Dal.MsSQL.T_MarkUserCertificate.Certificate(_markuser);
                return obj;
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updateuser(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addmarkuser _markuser = Untity.HelperJson.DeserializeObject<Entity.Request.addmarkuser>(postString);
                Entity.MsSQL.T_MarkUser _model = new Entity.MsSQL.T_MarkUser()
                {
                    Id = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(_markuser.id)),
                    Name = Untity.HelperDataCvt.objToString(_markuser.name),
                    Level = Untity.HelperDataCvt.objToString(_markuser.level),
                    Password = Untity.HelperDataCvt.objToString(_markuser.pwd),
                };
                Dal.MsSQL.T_MarkUser.Update(_model).ToString();
                Dal.MsSQL.T_MarkUserCertificate.Certificate(_markuser);
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updateuserpwd(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addmarkuser _markuser = Untity.HelperJson.DeserializeObject<Entity.Request.addmarkuser>(postString);
                long _id = Untity.HelperDataCvt.strToLong(_markuser.id);
                string _password = Untity.HelperDataCvt.objToString(_markuser.pwd);
                if (!string.IsNullOrEmpty(_password))
                {
                    Entity.MsSQL.T_MarkUser model = Dal.MsSQL.T_MarkUser.GetModel(_id);
                    if (model != null)
                    {
                        Dal.MsSQL.T_MarkUser.UpdatePwd(_id, _password);
                        return 1;
                    }
                    else
                    {
                        error = "用户不存在";
                    }
                }
                else
                {
                    error = "密码不能为空";
                }
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return 1;
        }

        public static object deletestudent(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Respose.getmarkuser _markuser = Untity.HelperJson.DeserializeObject<Entity.Respose.getmarkuser>(postString);
                Dal.MsSQL.T_MarkUser.Delete(Untity.HelperDataCvt.objToString(_markuser.id));
                Dal.MsSQL.T_MarkUserCertificate.deleteMarkUserCertificate(_markuser);
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object getorganiza(string _uid, string _pwd, string _name, string page, string limit, ref long count, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                List<Entity.Respose.Markgetorganiza> result = new List<Entity.Respose.Markgetorganiza>();
                List<Entity.MsSQL.T_Organiza> list = Dal.MsSQL.T_Organiza.GetListByPage(_name, page, limit, ref count);
                foreach (var item in list)
                {
                    result.Add(new Entity.Respose.Markgetorganiza()
                    {
                        Id = item.Id.ToString(),
                        AppName = item.AppName,
                        ClassId = item.ClassId,
                        CreateTime = Untity.HelperDataCvt.DateTimeToStr(item.CreateTime),
                        Describe = item.Describe,
                        IsDel = item.IsDel.ToString(),
                        Name = item.Name,
                        Password = item.Password,
                        Path = item.Path,
                    });
                }
                return result;
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object addorganiza(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelorganiza _organiza = Untity.HelperJson.DeserializeObject<Entity.Request.handelorganiza>(postString);
                Entity.MsSQL.T_Organiza _model = new Entity.MsSQL.T_Organiza()
                {
                    Name = Untity.HelperDataCvt.objToString(_organiza.Name),
                    Password = Untity.HelperDataCvt.objToString(_organiza.Password),
                    AppName = Untity.HelperDataCvt.objToString(_organiza.AppName),
                    ClassId = Untity.HelperDataCvt.objToString(_organiza.ClassId),
                    Path = Untity.HelperDataCvt.objToString(_organiza.Path),
                    Describe = Untity.HelperDataCvt.objToString(_organiza.Describe)
                };
                return Dal.MsSQL.T_Organiza.Add(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updateorganiza(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelorganiza _organiza = Untity.HelperJson.DeserializeObject<Entity.Request.handelorganiza>(postString);
                Entity.MsSQL.T_Organiza _model = new Entity.MsSQL.T_Organiza()
                {
                    Name = Untity.HelperDataCvt.objToString(_organiza.Name),
                    //Password = Untity.HelperDataCvt.objToString(_organiza.Password),
                    AppName = Untity.HelperDataCvt.objToString(_organiza.AppName),
                    ClassId = Untity.HelperDataCvt.objToString(_organiza.ClassId),
                    Path = Untity.HelperDataCvt.objToString(_organiza.Path),
                    Describe = Untity.HelperDataCvt.objToString(_organiza.Describe),
                    Id = Untity.HelperDataCvt.strToLong(_organiza.ID)
                };
                return Dal.MsSQL.T_Organiza.Update(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updateorganizapwd(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelorganiza _organiza = Untity.HelperJson.DeserializeObject<Entity.Request.handelorganiza>(postString);
                long _id = Untity.HelperDataCvt.strToLong(_organiza.ID);
                string _password = Untity.HelperDataCvt.objToString(_organiza.Password);
                if (!string.IsNullOrEmpty(_password))
                {
                    Entity.MsSQL.T_Organiza model = Dal.MsSQL.T_Organiza.GetModel(_id);
                    if (model != null)
                    {
                        Dal.MsSQL.T_Organiza.UpdatePwd(_id, _password);
                        return 1;
                    }
                    else
                    {
                        error = "用户不存在";
                    }
                }
                else
                {
                    error = "密码不能为空";
                }
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return 1;
        }

        public static object deleteorganiza(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelorganiza _organiza = Untity.HelperJson.DeserializeObject<Entity.Request.handelorganiza>(postString);
                Dal.MsSQL.T_Organiza.Delete(Untity.HelperDataCvt.objToString(_organiza.ID));
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object hangye(string _uid, string _pwd, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {

            }
            return null;
        }

        public static object hangyestudent(string _uid, string _pwd, string _hangye, string page, string limit, ref long count, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
            }
            return null;
        }

        public static object getallsubject(string _uid, string _pwd, string _name, string _page, string _limit, ref long count, ref string error)
        {
            List<Entity.MsSQL.T_Subject> result = new List<Entity.MsSQL.T_Subject>();
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                result = Dal.MsSQL.T_Subject.GetList(_name, _page, _limit, ref count);
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return result;
        }

        public static object getsubjectsbycertid(string _uid, string _pwd, string _certid, string _page, string _limit, ref long count, ref string error)
        {
            List<Entity.Respose.subjectsbycertid> result = new List<Entity.Respose.subjectsbycertid>();
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                result = Dal.MsSQL.T_CertifiSubject.GetListByCertId(_certid, _page, _limit, ref count);
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return result;
        }

        public static object addsubject(string _uid, string _pwd,string _path, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelsubject _subject = Untity.HelperJson.DeserializeObject<Entity.Request.handelsubject>(postString);
                Entity.MsSQL.T_Subject _model = new Entity.MsSQL.T_Subject()
                {
                    Name = _subject.Name,
                    Category = _subject.Category,
                    Price = _subject.Price,
                    Describe = _subject.Describe,
                    OLSchoolId = _subject.OLSchoolId,
                    OLSchoolAOMid = _subject.OLSchoolAOMid,
                    OLSchoolCourseId = _subject.OLSchoolCourseId,
                    OLSchoolMasterTypeId = _subject.OLSchoolMasterTypeId,
                    OLSchoolName = _subject.OLSchoolName,
                    OLSchoolProvinceId = _subject.OLSchoolProvinceId,
                    OLSchoolQuestionNum = _subject.OLSchoolQuestionNum,
                    OLAccCourseId = getAccCourseId(_subject.OLSchoolId, _path),
                    OLPaperID = _subject.OLPaperID
                };
                return Dal.MsSQL.T_Subject.Add(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updatesubject(string _uid, string _pwd, string _path, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelsubject _subject = Untity.HelperJson.DeserializeObject<Entity.Request.handelsubject>(postString);
                Entity.MsSQL.T_Subject _model = new Entity.MsSQL.T_Subject()
                {
                    ID = Untity.HelperDataCvt.strToLong(_subject.ID),
                    Name = _subject.Name,
                    Category = _subject.Category,
                    Price = _subject.Price,
                    Describe = _subject.Describe,
                    OLSchoolId = _subject.OLSchoolId,
                    OLSchoolAOMid = _subject.OLSchoolAOMid,
                    OLSchoolCourseId = _subject.OLSchoolCourseId,
                    OLSchoolMasterTypeId = _subject.OLSchoolMasterTypeId,
                    OLSchoolName = _subject.OLSchoolName,
                    OLSchoolProvinceId = _subject.OLSchoolProvinceId,
                    OLSchoolQuestionNum = _subject.OLSchoolQuestionNum,
                    OLAccCourseId = getAccCourseId(_subject.OLSchoolId, _path),
                    OLPaperID = _subject.OLPaperID
                };
                return Dal.MsSQL.T_Subject.Update(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static string getAccCourseId(string _OLSchoolId, string _path)
        {
            string AccCourseId = "";
            string joso = File.ReadAllText(_path + "/pages/datas/ExamToAcc.json");
            List<Entity.Respose.ExamToAcc> _ealist = Untity.HelperJson.DeserializeObject < List<Entity.Respose.ExamToAcc>>(joso);
            if (_ealist.Count > 0)
            {
                foreach(Entity.Respose.ExamToAcc item in _ealist)
                {
                    if (item.ExamCourseId == _OLSchoolId)
                    {
                        AccCourseId = item.AccCourseId;
                        break;
                    }
                }
            }
            return AccCourseId;
        }

        public static object delsubject(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelsubject _subject = Untity.HelperJson.DeserializeObject<Entity.Request.handelsubject>(postString);
                Dal.MsSQL.T_Subject.Delete(Untity.HelperDataCvt.objToString(_subject.ID));
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object addcertifisubject(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelcertifisubject _certifisubject = Untity.HelperJson.DeserializeObject<Entity.Request.handelcertifisubject>(postString);
                long existcount = Dal.MsSQL.T_CertifiSubject.GetCertifiSubjectCount(_certifisubject.ID, _certifisubject.CertificateId, _certifisubject.SubjectId);
                if (existcount > 0)
                {
                    error = "该课程此证书已包含，无法继续操作";
                    return "-1";
                }
                Entity.MsSQL.T_CertifiSubject _model = new Entity.MsSQL.T_CertifiSubject()
                {
                    CertificateId = _certifisubject.CertificateId,
                    SubjectId = _certifisubject.SubjectId,
                    NormalResult = _certifisubject.NormalResult,
                    ExamResult = _certifisubject.ExamResult,
                    IsNeedExam = _certifisubject.IsNeedExam,
                    ExamLength = Untity.HelperDataCvt.strToIni(_certifisubject.ExamLength)
                };
                return Dal.MsSQL.T_CertifiSubject.Add(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updatecertifisubject(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelcertifisubject _certifisubject = Untity.HelperJson.DeserializeObject<Entity.Request.handelcertifisubject>(postString);
                long existcount = Dal.MsSQL.T_CertifiSubject.GetCertifiSubjectCount(_certifisubject.ID, _certifisubject.CertificateId, _certifisubject.SubjectId);
                if (existcount > 0)
                {
                    error = "该课程此证书已包含，无法继续操作";
                    return "-1";
                }
                Entity.MsSQL.T_CertifiSubject _model = new Entity.MsSQL.T_CertifiSubject()
                {
                    ID = Untity.HelperDataCvt.strToLong(_certifisubject.ID),
                    SubjectId = _certifisubject.SubjectId,
                    NormalResult = _certifisubject.NormalResult,
                    ExamResult = _certifisubject.ExamResult,
                    IsNeedExam = _certifisubject.IsNeedExam,
                    ExamLength = Untity.HelperDataCvt.strToIni(_certifisubject.ExamLength)
                };
                return Dal.MsSQL.T_CertifiSubject.Update(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object delcertifisubject(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.handelcertifisubject _certifisubject = Untity.HelperJson.DeserializeObject<Entity.Request.handelcertifisubject>(postString);
                Dal.MsSQL.T_CertifiSubject.Delete(Untity.HelperDataCvt.objToString(_certifisubject.ID));
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object getallsubjectnopage(string _uid, string _pwd, ref string error)
        {
            List<Entity.MsSQL.T_Subject> result = new List<Entity.MsSQL.T_Subject>();
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                result = Dal.MsSQL.T_Subject.GetAllList();
            }
            else
            {
                error = "账号失效，请重新登陆";
            }
            return result;
        }

        public static object addcertificate(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addcertificate _certificate = Untity.HelperJson.DeserializeObject<Entity.Request.addcertificate>(postString);
                Entity.MsSQL.T_Certificate _model = new Entity.MsSQL.T_Certificate()
                {
                    CategoryName = _certificate.CategoryName,
                    ExamSubject = _certificate.ExamSubject,
                    StartTime = Untity.HelperDataCvt.strToDatetime(_certificate.StartTime),
                    EndTime = Untity.HelperDataCvt.strToDatetime(_certificate.EndTime),
                    Describe = _certificate.Describe,
                    ExamResult = _certificate.ExamResult,
                    NormalResult = _certificate.NormalResult
                };
                long _id = Dal.MsSQL.T_Certificate.Add(_model);
                if (!string.IsNullOrEmpty(_certificate.SubjectIds))
                {
                    foreach (var _subjcetid in _certificate.SubjectIds.Split(','))
                    {
                        Entity.MsSQL.T_CertifiSubject _subject = new Entity.MsSQL.T_CertifiSubject()
                        {

                        };

                    }
                }
                return _id.ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updatecertificate(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addcertificate _certificate = Untity.HelperJson.DeserializeObject<Entity.Request.addcertificate>(postString);
                Entity.MsSQL.T_Certificate _model = new Entity.MsSQL.T_Certificate()
                {
                    Id = Untity.HelperDataCvt.strToLong(_certificate.Id),
                    CategoryName = _certificate.CategoryName,
                    ExamSubject = _certificate.ExamSubject,
                    StartTime = Untity.HelperDataCvt.strToDatetime(_certificate.StartTime),
                    EndTime = Untity.HelperDataCvt.strToDatetime(_certificate.EndTime),
                    Describe = _certificate.Describe,
                    ExamResult = _certificate.ExamResult,
                    NormalResult = _certificate.NormalResult
                };
                return Dal.MsSQL.T_Certificate.Update(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object delcertificate(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addcertificate _certificate = Untity.HelperJson.DeserializeObject<Entity.Request.addcertificate>(postString);
                Dal.MsSQL.T_Certificate.Delete(Untity.HelperDataCvt.objToString(_certificate.Id));
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object getMarkUserCertificateById(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                Entity.Request.addmarkuser _markuser = Untity.HelperJson.DeserializeObject<Entity.Request.addmarkuser>(postString);

                return Dal.MsSQL.T_MarkUserCertificate.getMarkUserCertificateById(_markuser.id);
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object getMarkUserCertificateByName(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                return Dal.MsSQL.T_MarkUserCertificate.getMarkUserCertificateByName(postString);
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }


        public static object getStudentsByCertificateID(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_MarkUser _user = Dal.MsSQL.T_MarkUser.GetModel(_uid, _pwd);
            if (_user != null)
            {
                return Dal.MsSQL.T_StudentTicket.getStudentsByCertificateID(postString);
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

    }
}
