using JlueCertificate.Entity.Enum;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_StudentSubjectScore
    {
        public string id { get; set; }

        public string studentid { get; set; }

        public string studentticketid { get; set; }

        public string sortid { get; set; }

        public string score { get; set; }

        public string isexam { get; set; }

        public string createtime { get; set; }

        private static SqlSugarClient db
        {
            get
            {
                ConnectionConfig connectionConfig = new ConnectionConfig()
                {
                    ConnectionString = Untity.HelperMsSQL.connStr,
                    DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                };
                return new SqlSugarClient(connectionConfig);
            }
        }

        public static object getStudentSubjectScore(string studentid, string sortid)
        {
            var query = db.Queryable<T_StudentSubjectScore>().Where(a => a.studentid == studentid && a.sortid == sortid);
            return query;
        }
        public static List<T_StudentSubjectScore> getIsexaminSubjectScore(string postString)
        {
            Dal.MsSQL.T_StudentSubjectScore sss = Untity.HelperJson.DeserializeObject<Dal.MsSQL.T_StudentSubjectScore>(postString);

            List<T_StudentSubjectScore> list = db.Queryable<T_StudentSubjectScore>().Where(a => a.studentticketid == sss.studentticketid && a.sortid == sss.sortid).ToList();
            return list;
        }
        public static List<T_StudentSubjectScore> getStudentSubjectExamScore(string studentid, string sortid, string studentticketid)
        {
            List<T_StudentSubjectScore> list = db.Queryable<T_StudentSubjectScore>().Where(a => a.studentid == studentid && a.sortid == sortid && a.studentticketid == studentticketid && a.isexam == "1").ToList().OrderByDescending(ii => ii.createtime).ToList();
            return list;
        }

        public static object addStudentSubjectScore(T_StudentSubjectScore sss)
        {
            var obj = db.Insertable<T_StudentSubjectScore>(sss).ExecuteCommand();
            return obj;
        }

        public static object updateStudentSubjectScore(T_StudentSubjectScore sss)
        {
            // var obj = db.Updateable<T_StudentSubjectScore>(sss).ExecuteCommand();
           // var obj = db.Updateable<T_StudentSubjectScore>().UpdateColumns(it => new { score = sss.score}).Where(it => it.studentticketid == sss.studentticketid).ExecuteCommand();
            string sql = string.Format("UPDATE dbo.T_StudentSubjectScore SET score = '{0}' WHERE studentticketid = '{1}' AND sortid = '{2}'", sss.score,sss.studentticketid,sss.sortid);
            Untity.HelperMsSQL.ExecuteQuery(sql);
            return 1;
        }

        public static List<Entity.Respose.scoredetail> getscore(List<Entity.Respose.allcertifisubject> _certifisubjectlist, string classid, string OLSchoolUserId, string StudentTicketId)
        {
            List<Entity.Respose.scoredetail> result = new List<Entity.Respose.scoredetail>();
            foreach (var item in _certifisubjectlist)
            {
                Entity.Respose.scoredetail _score = new Entity.Respose.scoredetail();
                _score.AOMid = item.OLSchoolAOMid;
                _score.Category = item.Category;
                _score.Name = item.Name;
                _score.NormalResult = item.NormalResult;
                _score.ExamResult = item.ExamResult;

                if (item.Category == SubjectCategory.题库)
                {
                    decimal normalscore = GetTiKuNormalScore(classid, OLSchoolUserId, item.OLSchoolId);
                    _score.NormalScore = normalscore.ToString();
                    decimal examscore = GetTiKuExamScore(classid, OLSchoolUserId, item.OLPaperID);
                    _score.ExamScore = examscore.ToString();
                }
                else if (item.Category == SubjectCategory.视频)
                {
                    decimal normalscore = GetSPNormalScore(classid,OLSchoolUserId, item.OLSchoolAOMid, item.OLSchoolId);
                    _score.NormalScore = normalscore.ToString();
                    _score.ExamScore = "0";
                }
                else if (item.Category == SubjectCategory.实操报税)
                {
                    decimal normalscore = GetBSNormalScore(classid, OLSchoolUserId, item.OLSchoolId, "");
                    _score.NormalScore = normalscore.ToString();
                    decimal examscore = GetBSExamScore(OLSchoolUserId, StudentTicketId, item.OLSchoolId);
                    _score.ExamScore = examscore.ToString();
                }
                else if (item.Category == SubjectCategory.实操电脑账)
                {
                    decimal normalscore = GetDNZNormalScore(classid, OLSchoolUserId, item.OLSchoolId, item.OLAccCourseId);
                    _score.NormalScore = normalscore.ToString();
                    decimal examscore = GetDNZExamScore(OLSchoolUserId, StudentTicketId, item.OLSchoolId);
                    _score.ExamScore = examscore.ToString();
                }
                result.Add(_score);
            }

            return result;
        }

        public static List<Entity.Respose.normalscore> getnormalscore(List<Entity.Respose.allcertifisubject> _certifisubjectlist, string classid, string OLSchoolUserId, string StudentTicketId)
        {
            List<Entity.Respose.normalscore> result = new List<Entity.Respose.normalscore>();
            int index = 0;
            foreach (var item in _certifisubjectlist)
            {
                Entity.Respose.normalscore _score = new Entity.Respose.normalscore();

                _score.subjectId = item.SubjectId;
                _score.subjectName = item.Name;
                _score.subjectType = item.Category;
                _score.Sort_Id = item.OLSchoolId;
                _score.AOMid = item.OLSchoolAOMid;
                _score.classId = classid;
                _score.index = ++index;
                if (item.Category == SubjectCategory.题库)
                {
                    decimal normalscore = GetTiKuNormalScore(classid, OLSchoolUserId, item.OLSchoolId);
                    _score.score = normalscore;

                }
                else if (item.Category == SubjectCategory.视频)
                {
                    decimal normalscore = GetSPNormalScore(classid,OLSchoolUserId, item.OLSchoolAOMid, item.OLSchoolId);
                    _score.score = normalscore;
                }
                else if (item.Category == SubjectCategory.实操报税)
                {
                    decimal normalscore = GetBSNormalScore(classid, OLSchoolUserId, item.OLSchoolId, "");
                    _score.score = normalscore;

                }
                else if (item.Category == SubjectCategory.实操电脑账)
                {
                    decimal normalscore = GetDNZNormalScore(classid, OLSchoolUserId, item.OLSchoolId, item.OLAccCourseId);
                    _score.score = normalscore;
                }
                result.Add(_score);
            }
            return result;
        }

        /// <summary>
        /// 获取题库平时成绩
        /// </summary>
        public static decimal GetTiKuNormalScore(string classid, string OLSchoolUserId, string OLSchoolId)
        {
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = Untity.HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Tiku/Paper/CoursePaperList?classid=" + classid
                + "&userid=" + OLSchoolUserId
                + "&sortid=" + OLSchoolId;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            List<Entity.Respose.Sorts> _sorts = Untity.HelperJson.DeserializeObject<List<Entity.Respose.Sorts>>(result.Data.ToString());
            decimal score = 0;
            foreach (Entity.Respose.Sorts sortitem in _sorts)
            {
                if (sortitem.name == "模拟考试" && sortitem.papers.Count > 0)
                {
                    foreach (Entity.Respose.Papers paperitem in sortitem.papers)
                    {
                        if (paperitem.scores.Count > 0)
                        {
                            score = paperitem.scores.Max(x => x.Score);
                        }
                    }
                }
            }
            return score;
        }

        /// <summary>
        /// 获取题库考试成绩
        /// </summary>
        public static decimal GetTiKuExamScore(string classid, string OLSchoolUserId, string OLPaperID)
        {
            decimal score = 0;
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = Untity.HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Tiku/Paper/GetCGXExamPaperList?classid=" + classid
                + "&userid=" + OLSchoolUserId
                + "&paperid=" + OLPaperID;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            List<Entity.Respose.PaperScores> _sorts = Untity.HelperJson.DeserializeObject<List<Entity.Respose.PaperScores>>(result.Data.ToString());
            if (_sorts.Count > 0)
            {
                score = _sorts.LastOrDefault().Score;
            }
            return score;
        }

        /// <summary>
        /// 获取视频平时成绩
        /// </summary>
        public static decimal GetSPNormalScore(string classid,string OLSchoolUserId, string OLSchoolAOMid, string OLSchoolId)
        {
            decimal score = 0;
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = Untity.HelperAppSet.getAppSetting("wangxiaohost");
            string fullpath = path + "/api/VideoJinDu/Check?classid=" + classid + "&Sort_Id=" + OLSchoolId
                + "&studentid=" + OLSchoolUserId
                + "&AOMid=" + OLSchoolAOMid
                + "&PublicMark=" + true;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult2 result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult2>(json);
            Entity.Respose.SPScore _spscore = Untity.HelperJson.DeserializeObject<Entity.Respose.SPScore>(result.data.ToString());
            if (_spscore != null)
            {
                score = _spscore.persent;
            }
            return score;
        }

        /// <summary>
        /// 获取报税平时成绩
        /// </summary>
        public static decimal GetBSNormalScore(string classid, string OLSchoolUserId, string OLSchoolId, string OLPaperID)
        {
            decimal score = 0;
            return score;
        }

        /// <summary>
        /// 获取报税考试成绩
        /// </summary>
        public static decimal GetBSExamScore(string OLSchoolUserId, string StudentTicketId, string sortid)
        {
            decimal score = 0;
            var list = getStudentSubjectExamScore(OLSchoolUserId, sortid, StudentTicketId).FirstOrDefault();
            if (list != null)
            {
                score = Convert.ToDecimal(list.score);
            }
            return score;
        }

        /// <summary>
        /// 获取电脑账平时成绩
        /// </summary>
        public static decimal GetDNZNormalScore(string classid, string OLSchoolUserId, string OLSchoolId, string OLAccCourseId)
        {
            decimal score = 0;
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = Untity.HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Member/GetHKJNormalScore?classid=" + classid
                + "&OLSchoolUserId=" + OLSchoolUserId
                + "&OLSchoolId=" + OLSchoolId
                + "&OLAccCourseId=" + OLAccCourseId;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            List<Entity.Respose.HKJScore> _sorts = Untity.HelperJson.DeserializeObject<List<Entity.Respose.HKJScore>>(result.Data.ToString());
            if (_sorts.Count > 0)
            {
                score = _sorts.Average(ii => ii.AutoFinal);
            }
            return score;
        }

        /// <summary>
        /// 获取电脑账考试成绩
        /// </summary>
        public static decimal GetDNZExamScore(string OLSchoolUserId, string StudentTicketId, string sortid)
        {
            decimal score = 0;
            var list = getStudentSubjectExamScore(OLSchoolUserId, sortid, StudentTicketId).FirstOrDefault();
            if (list != null)
            {
                score = Convert.ToDecimal(list.score);
            }
            return score;
        }
    }
}
