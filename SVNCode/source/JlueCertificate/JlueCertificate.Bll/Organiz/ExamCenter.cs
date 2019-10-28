using JlueCertificate.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Bll.Organiz
{
    public class ExamCenter
    {
        public static object getStudent(string _uid, string _pwd, string _name, string _cardid, string page, string limit, ref long count, ref string error)
        {
            List<Entity.Respose.getstudent> result = new List<Entity.Respose.getstudent>();
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                result = ConvertStudentToResponse(Dal.MsSQL.T_Student.GetListByPage(_orga.Id, _name, _cardid, page, limit, ref count));
            }

            return result;
        }

        private static List<Entity.Respose.getstudent> ConvertStudentToResponse(List<Entity.MsSQL.T_Student> list)
        {
            List<Entity.Respose.getstudent> result = new List<Entity.Respose.getstudent>();
            int i = 1;
            list.ForEach(ii =>
            {
                result.Add(new Entity.Respose.getstudent()
                {
                    id = (i++).ToString(),
                    idnumber = Untity.HelperDataCvt.objToString(ii.Id),
                    name = Untity.HelperDataCvt.objToString(ii.Name),
                    cardid = Untity.HelperDataCvt.objToString(ii.CardId),
                    address = Untity.HelperDataCvt.objToString(ii.Address),
                    cityid = Untity.HelperDataCvt.objToString(ii.CityId),
                    headerurl = Untity.HelperDataCvt.objToString(ii.HeaderUrl),
                    postaddress = Untity.HelperDataCvt.objToString(ii.PostAddress),
                    postcityid = Untity.HelperDataCvt.objToString(ii.PostCityId),
                    postprovinceid = Untity.HelperDataCvt.objToString(ii.PostProvinceId),
                    postzoneid = Untity.HelperDataCvt.objToString(ii.PostZoneId),
                    provinceid = Untity.HelperDataCvt.objToString(ii.ProvinceId),
                    sex = Untity.HelperDataCvt.objToString(ii.Sex) == "1" ? "男" : "女",
                    telphone = Untity.HelperDataCvt.objToString(ii.TelPhone),
                    zoneid = Untity.HelperDataCvt.objToString(ii.ZoneId),
                    createtime = ii.CreateTime.ToString("yyyy-MM-dd"),
                    olschoolusername = ii.OLSchoolUserName
                });
            });
            return result;
        }

        public static object getStudentId(string _uid, string _pwd)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            return string.Format("{0}{1}{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), _orga == null ? 0 : _orga.Id, new Random().Next(10, 99));
        }

        public static object getZKH(string orgaizid)
        {
            return string.Format("{0}{1}{2}", "ZKH" + DateTime.Now.ToString("yyMMddHHmmss"), string.IsNullOrEmpty(orgaizid) ? "0" : orgaizid, new Random().Next(10, 99));
        }

        public static object addstudent(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Request.addstudent _student = Untity.HelperJson.DeserializeObject<Entity.Request.addstudent>(postString);
                Entity.MsSQL.T_Student _model = new Entity.MsSQL.T_Student()
                {
                    Id = Untity.HelperDataCvt.objToString(_student.idnumber),
                    OrgaId = _orga.Id,
                    Name = Untity.HelperDataCvt.objToString(_student.name),
                    CardId = Untity.HelperDataCvt.objToString(_student.cardid),
                    HeaderUrl = Untity.HelperDataCvt.objToString(_student.headerurl),
                    Sex = Untity.HelperDataCvt.objToString(_student.sex),
                    TelPhone = Untity.HelperDataCvt.objToString(_student.telphone),
                    ProvinceId = Untity.HelperDataCvt.objToString(_student.provinceid),
                    CityId = Untity.HelperDataCvt.objToString(_student.cityid),
                    ZoneId = Untity.HelperDataCvt.objToString(_student.zoneid),
                    Address = Untity.HelperDataCvt.objToString(_student.address),
                    PostProvinceId = Untity.HelperDataCvt.objToString(_student.postprovinceid),
                    PostCityId = Untity.HelperDataCvt.objToString(_student.postcityid),
                    PostZoneId = Untity.HelperDataCvt.objToString(_student.postzoneid),
                    PostAddress = Untity.HelperDataCvt.objToString(_student.postaddress)
                };
                return Dal.MsSQL.T_Student.Add(_model).ToString();
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object updatestudent(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Request.addstudent _student = Untity.HelperJson.DeserializeObject<Entity.Request.addstudent>(postString);
                Entity.MsSQL.T_Student _model = new Entity.MsSQL.T_Student()
                {
                    Id = Untity.HelperDataCvt.objToString(_student.idnumber),
                    OrgaId = _orga.Id,
                    Name = Untity.HelperDataCvt.objToString(_student.name),
                    CardId = Untity.HelperDataCvt.objToString(_student.cardid),
                    HeaderUrl = Untity.HelperDataCvt.objToString(_student.headerurl),
                    Sex = Untity.HelperDataCvt.objToString(_student.sex),
                    TelPhone = Untity.HelperDataCvt.objToString(_student.telphone),
                    ProvinceId = Untity.HelperDataCvt.objToString(_student.provinceid),
                    CityId = Untity.HelperDataCvt.objToString(_student.cityid),
                    ZoneId = Untity.HelperDataCvt.objToString(_student.zoneid),
                    Address = Untity.HelperDataCvt.objToString(_student.address),
                    PostProvinceId = Untity.HelperDataCvt.objToString(_student.postprovinceid),
                    PostCityId = Untity.HelperDataCvt.objToString(_student.postcityid),
                    PostZoneId = Untity.HelperDataCvt.objToString(_student.postzoneid),
                    PostAddress = Untity.HelperDataCvt.objToString(_student.postaddress)
                };
                Dal.MsSQL.T_Student.Update(_model).ToString();
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object deletestudent(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Request.addstudent _student = Untity.HelperJson.DeserializeObject<Entity.Request.addstudent>(postString);
                Dal.MsSQL.T_Student.Delete(Untity.HelperDataCvt.objToString(_student.idnumber));
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object getsubjectsbyids(string _uid, string _pwd, string _subjectids, ref string error)
        {
            Entity.Respose.getsubjectsbycertid result = new Entity.Respose.getsubjectsbycertid();
            List<Entity.MsSQL.T_Subject> _allsubject = new List<Entity.MsSQL.T_Subject>();
            List<Entity.Respose.subjectsbycertid> _allbycertid = new List<Entity.Respose.subjectsbycertid>();
            _allsubject = Dal.MsSQL.T_Subject.GetAllList();
            foreach (Entity.MsSQL.T_Subject item in _allsubject)
            {
                Entity.Respose.subjectsbycertid newsub = new Entity.Respose.subjectsbycertid();
                newsub.ID = Untity.HelperDataCvt.objToString(item.ID);
                newsub.Category = Untity.HelperDataCvt.objToString(item.Category);
                newsub.Name = Untity.HelperDataCvt.objToString(item.Name);
                newsub.Price = Untity.HelperDataCvt.objToString(item.Price);
                newsub.LAY_CHECKED = _subjectids.Split(',').Contains(Untity.HelperDataCvt.objToString(item.ID));
                _allbycertid.Add(newsub);
            }
            result.all = _allbycertid;
            return result;
        }

        public static object getallcertificate(string _uid, string _pwd, string _name, string page, string limit, ref long count, ref string error)
        {
            List<Entity.Respose.Markgetallcertificate> result = new List<Entity.Respose.Markgetallcertificate>();
            List<Entity.MsSQL.T_Certificate> list = Dal.MsSQL.T_Certificate.GetList(_name, page, limit, ref count);
            foreach (var item in list)
            {
                result.Add(new Entity.Respose.Markgetallcertificate()
                {
                    StartTime = Untity.HelperDataCvt.DateTimeToStrNoHour(item.StartTime),
                    ExamSubject = item.ExamSubject,
                    CategoryName = item.CategoryName,
                    CreateTime = Untity.HelperDataCvt.DateTimeToStr(item.CreateTime),
                    Describe = item.Describe,
                    EndTime = Untity.HelperDataCvt.DateTimeToStrNoHour(item.EndTime),
                    ExamResult = item.ExamResult,
                    Id = item.Id.ToString(),
                    IsDel = item.IsDel,
                    NormalResult = item.NormalResult,
                    Rule = item.Rule
                });
            }
            return result;
        }

        public static object getunsignupcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit, ref long count, ref string error)
        {
            Entity.Respose.getunsignupcertificate result = new Entity.Respose.getunsignupcertificate();
            List<Entity.Respose.unsignupcertificate> _allunsign = new List<Entity.Respose.unsignupcertificate>();
            if (string.IsNullOrEmpty(_name) && string.IsNullOrEmpty(_cardid))
            {

            }
            else
            {
                Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(_name, _cardid);
                if (_student != null)
                {
                    List<Entity.MsSQL.T_StudentTicket> _getticket = new List<Entity.MsSQL.T_StudentTicket>();
                    List<Entity.Respose.allcertifisubject> _allcertifisubject = new List<Entity.Respose.allcertifisubject>();
                    _getticket = Dal.MsSQL.T_StudentTicket.GetList(_student.Id);
                    string _allCertificateids = string.Join(",", _getticket.Select(ii => ii.CertificateId.ToString()).ToList());
                    _allunsign = Dal.MsSQL.T_Certificate.GetUnsignupList(_allCertificateids, page, limit, ref count);
                    _allcertifisubject = Dal.MsSQL.T_CertifiSubject.GetAllList();
                    foreach (Entity.Respose.unsignupcertificate item in _allunsign)
                    {
                        item.certifisubjects = _allcertifisubject.Where(i => i.CertificateId == item.Id).ToList();
                    }
                    result.unsignup = _allunsign;
                    result.name = _student.Name;
                    result.cardid = _student.CardId;
                    result.username = _student.OLSchoolUserName;
                }
                else
                {
                    error = "学员不存在，请核对信息是否正确";
                }
            }
            return result;
        }

        public static object getsignupcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit, ref long count, ref string error)
        {
            Entity.Respose.getsignupcertificate result = new Entity.Respose.getsignupcertificate();
            List<Entity.Respose.signupcertificate> _allsign = new List<Entity.Respose.signupcertificate>();
            if (string.IsNullOrEmpty(_name) && string.IsNullOrEmpty(_cardid))
            {
            }
            else
            {
                Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(_name, _cardid);
                if (_student != null)
                {
                    _allsign = Dal.MsSQL.T_Certificate.GetSignUpList(_student.Id, CertState.已报名, page, limit, ref count);
                    result.signup = _allsign;
                    result.name = _student.Name;
                    result.cardid = _student.CardId;
                }
                else
                {
                    error = "学员不存在，请核对信息是否正确";
                }
            }
            return result;
        }

        public static object getholdcertificate(string _uid, string _pwd, string _name, string _cardid, string page, string limit, ref long count, ref string error)
        {
            Entity.Respose.getholdcertificate result = new Entity.Respose.getholdcertificate();
            List<Entity.Respose.holdcertificate> _allhold = new List<Entity.Respose.holdcertificate>();
            if (string.IsNullOrEmpty(_name) && string.IsNullOrEmpty(_cardid))
            {
            }
            else
            {
                Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(_name, _cardid);
                if (_student != null)
                {
                    _allhold = Dal.MsSQL.T_Certificate.GetHoldList(_student.Id, CertState.已发证, page, limit, ref count);
                    result.hold = _allhold;
                    result.name = _student.Name;
                    result.cardid = _student.CardId;
                }
                else
                {
                    error = "学员不存在，请核对信息是否正确";
                }
            }
            return result;
        }

        public static object signup(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Request.signup _signup = Untity.HelperJson.DeserializeObject<Entity.Request.signup>(postString);
                Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(_signup.studentid);
                if (_student != null)
                {
                    //判断当前证书是否已经报名
                    long tccount = Dal.MsSQL.T_StudentTicket.GetTicketCount(_student.Id, _signup.certificateid.ToString());
                    if (tccount > 0)
                    {
                        error = "已报名，请误重复报名";
                        return tccount;
                    }
                    //验证账号合法性
                    if (string.IsNullOrEmpty(_student.OLSchoolUserId))
                    {
                        error = "尚未绑定网校账号，无法报名";
                        return "-1";
                    }
                    //判断当前课程是否全部购买
                    Entity.MsSQL.T_Certificate _certificate = Dal.MsSQL.T_Certificate.GetModel(_signup.certificateid);
                    if (_certificate != null)
                    {
                        List<Entity.Respose.allcertifisubject> _sublist = new List<Entity.Respose.allcertifisubject>();
                        _sublist = Dal.MsSQL.T_CertifiSubject.GetAllListByCertId(Untity.HelperDataCvt.objToString(_certificate.Id));
                        string ids = string.Join(",", _sublist.Select(ii => ii.OLSchoolAOMid.ToString()).ToList());
                        if (ids != "" && !(Dal.MsSQL.T_Subject.IsBuyAll(_orga, ids, _signup.username, ref error)))
                        {
                            error = error + ",无法报名";
                            return "-1";
                        }
                        Entity.MsSQL.T_StudentTicket _model = new Entity.MsSQL.T_StudentTicket()
                        {
                            CertificateId = _signup.certificateid.ToString(),
                            OrgaizId = _orga.Id,
                            StudentId = _student.Id,
                            TicketNum = getZKH(Untity.HelperDataCvt.objToString(_orga.Id)).ToString(),
                            OLMobile = ""
                        };
                        return Dal.MsSQL.T_StudentTicket.Add(_model).ToString();
                    }
                    else
                    {
                        error = "证书不存在，请核对信息是否正确";
                    }
                }
                else
                {
                    error = "学员不存在，请核对信息是否正确";
                }
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object cancel(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Respose.studentcertifi _cancel = Untity.HelperJson.DeserializeObject<Entity.Respose.studentcertifi>(postString);
                Dal.MsSQL.T_StudentTicket.Delete(_cancel.TicketNum);
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        public static object bangding(string _uid, string _pwd, string postString, ref string error)
        {
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                Entity.Request.bangding _bangding = Untity.HelperJson.DeserializeObject<Entity.Request.bangding>(postString);
                Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(_bangding.studentid);
                if (_student != null)
                {
                    //判定当前网校账号是否已经绑定现有学生账号
                    long olcount = Dal.MsSQL.T_Student.GetOLSchoolUserCount(_student.Id, _bangding.olschoolusername, Untity.HelperDataCvt.objToString(_orga.Id));
                    if (olcount > 0)
                    {
                        error = "此网校账号已经被其他学员绑定，无法报名";
                        return olcount;
                    }
                    //验证账号合法性
                    Entity.Respose.OLUserResponse oluser = Dal.MsSQL.T_Student.getOLSchoolUserId(_orga, _bangding.olschoolusername, _bangding.olschoolpwd);
                    if (string.IsNullOrEmpty(oluser.id))
                    {
                        error = oluser.msg + "，无法报名";
                        return "-1";
                    }
                    //第一次绑定或修改绑定
                    if (string.IsNullOrEmpty(_student.OLSchoolUserId)
                        || Untity.HelperDataCvt.objToString(_student.OLSchoolUserId) != oluser.id)
                    {
                        Dal.MsSQL.T_Student.updateOLSchoolUserId(oluser.id, _bangding.olschoolusername, _bangding.olschoolpwd, _student.Id);
                    }
                }
                else
                {
                    error = "学员不存在，请核对信息是否正确";
                }
                return "1";
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }


        public static object getstudentcertifi(string _uid, string _pwd, string studentid, ref string error)
        {
            Entity.Respose.getstudentcertifi result = new Entity.Respose.getstudentcertifi();
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            if (_orga != null)
            {
                List<Entity.Respose.unsignupcertificate> _allunsign = new List<Entity.Respose.unsignupcertificate>();
                if (!string.IsNullOrEmpty(studentid))
                {
                    Entity.MsSQL.T_Student _student = Dal.MsSQL.T_Student.GetModel(studentid);
                    if (_student != null)
                    {
                        if (_orga.Id != _student.OrgaId)
                        {
                            error = "非本机构学员无法查询！";
                            return "-1";
                        }
                        result.cardid = _student.CardId;
                        result.name = _student.Name;
                        result.username = _student.OLSchoolUserName;
                        List<Entity.MsSQL.T_Certificate> _CertifisAll = Dal.MsSQL.T_Certificate.GetList();
                        List<Entity.MsSQL.T_Subject> _SubjectsAll = Dal.MsSQL.T_Subject.GetAllList();
                        List<Entity.MsSQL.T_CertifiSubject> _CertifiSubjectsAll = Dal.MsSQL.T_CertifiSubject.GetList();
                        List<Entity.MsSQL.T_StudentTicket> _TicketsAll = Dal.MsSQL.T_StudentTicket.GetList(studentid);
                        List<Entity.MsSQL.T_CertifiSerial> _SerialsAll = Dal.MsSQL.T_CertifiSerial.GetAllList();
                        foreach (var item in _TicketsAll)
                        {
                            Entity.MsSQL.T_Certificate _Certifi = _CertifisAll.Where(ii => ii.Id.ToString() == item.CertificateId).FirstOrDefault();
                            Entity.MsSQL.T_CertifiSerial _Serial = _SerialsAll.Where(ii => ii.SerialNum.ToString() == item.SerialNum).FirstOrDefault();
                            Entity.Respose.studentcertifi _model = ConvertStudentCertifiToResponse(_Certifi, item, _Serial, _CertifiSubjectsAll, _SubjectsAll);
                            if (_model == null)
                            {
                                continue;
                            }
                            if (string.IsNullOrEmpty(item.SerialNum))
                            {
                                result.signup.Add(_model);
                            }
                            else
                            {
                                result.hold.Add(_model);
                            }
                        }

                        foreach (var item in _CertifisAll)
                        {
                            if (item.StartTime > DateTime.Now)
                            {
                                continue;
                            }
                            if (item.EndTime < DateTime.Now && !string.IsNullOrEmpty(Untity.HelperDataCvt.DateTimeToStr(item.EndTime)))
                            {
                                continue;
                            }
                            Entity.Respose.studentcertifi _item1 = result.signup.Where(ii => ii.CertifiId == item.Id.ToString()).FirstOrDefault();
                            if (_item1 != null)
                            {
                                continue;
                            }
                            Entity.Respose.studentcertifi _item2 = result.hold.Where(ii => ii.CertifiId == item.Id.ToString()).FirstOrDefault();
                            if (_item2 != null)
                            {
                                continue;
                            }
                            Entity.Respose.studentcertifi _model = ConvertStudentCertifiToResponse(item, null, null, _CertifiSubjectsAll, _SubjectsAll);
                            if (_model != null)
                            {
                                result.unsignup.Add(_model);
                            }
                        }
                    }
                    else
                    {
                        error = "学员不存在，请核对信息是否正确";
                    }
                }
                return result;
            }
            else
            {
                error = "账号失效，请重新登陆";
                return "-1";
            }
        }

        private static Entity.Respose.studentcertifi ConvertStudentCertifiToResponse(Entity.MsSQL.T_Certificate _Certifi, Entity.MsSQL.T_StudentTicket _ticket,
            Entity.MsSQL.T_CertifiSerial _Serial, List<Entity.MsSQL.T_CertifiSubject> _CertifiSubjectsAll, List<Entity.MsSQL.T_Subject> _SubjectsAll)
        {
            if (_Certifi == null)
            {
                return null;
            }
            Entity.Respose.studentcertifi result = new Entity.Respose.studentcertifi()
            {
                SerialNum = _ticket == null ? "" : Untity.HelperDataCvt.objToString(_ticket.SerialNum),
                CategoryName = Untity.HelperDataCvt.objToString(_Certifi.CategoryName),
                TicketNum = _ticket == null ? "" : Untity.HelperDataCvt.objToString(_ticket.TicketNum),
                ExamSubject = Untity.HelperDataCvt.objToString(_Certifi.ExamSubject),
                Describe = Untity.HelperDataCvt.objToString(_Certifi.Describe),
                NormalResult = Untity.HelperDataCvt.objToString(_Certifi.NormalResult),
                ExamResult = Untity.HelperDataCvt.objToString(_Certifi.ExamResult),
                StartTime = Untity.HelperDataCvt.DateTimeToStrNoHour(_Certifi.StartTime),
                EndTime = Untity.HelperDataCvt.DateTimeToStrNoHour(_Certifi.EndTime),
                IssueDate = ((_Serial != null && _Serial.IssueDate != null) ? Untity.HelperDataCvt.DateTimeToStr(Convert.ToDateTime(_Serial.IssueDate)) : ""),
                CertState = CertState.未报名,
                CertifiId = _Certifi.Id.ToString(),
            };
            if (_ticket != null)
            {
                if (string.IsNullOrEmpty(_ticket.SerialNum))
                {
                    result.CertState = CertState.已报名;
                }
                else
                {
                    result.CertState = CertState.已发证;
                }
            }
            List<Entity.MsSQL.T_CertifiSubject> _CertifiSubjects = _CertifiSubjectsAll.Where(ii => ii.CertificateId == _Certifi.Id.ToString()).ToList();

            foreach (var item in _CertifiSubjects)
            {
                Entity.MsSQL.T_Subject _subject = _SubjectsAll.Where(ii => ii.ID.ToString() == item.SubjectId).FirstOrDefault();
                result.Subject.Add(new Entity.Respose.allcertifisubject()
                {
                    CertificateId = item.CertificateId,
                    Category = _subject == null ? "" : _subject.Category,
                    SubjectId = item.SubjectId,
                    NormalResult = Untity.HelperDataCvt.objToString(item.NormalResult),
                    ExamResult = Untity.HelperDataCvt.objToString(item.ExamResult),
                    Name = _subject == null ? "" : _subject.Name,
                    Price = _subject == null ? "" : _subject.Price,
                    OLSchoolId = _subject.OLSchoolId
                });
            }
            return result;
        }


        public static object getcertifiprint(string _uid, string _pwd, string _serialnum, ref string error)
        {
            Entity.Respose.getcertifiprint result = new Entity.Respose.getcertifiprint();
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);

            result = Dal.MsSQL.T_StudentTicket.GetCertifiPrintModel(_serialnum);
            return result;
        }

        public static object getticketprint(string _uid, string _pwd, string _ticketnum, ref string error)
        {
            Entity.Respose.getticketprint result = new Entity.Respose.getticketprint();
            Entity.MsSQL.T_Organiza _orga = Dal.MsSQL.T_Organiza.GetModel(_uid, _pwd);
            result = Dal.MsSQL.T_StudentTicket.GetTicketPrintModel(_ticketnum);
            return result;
        }
    }
}
