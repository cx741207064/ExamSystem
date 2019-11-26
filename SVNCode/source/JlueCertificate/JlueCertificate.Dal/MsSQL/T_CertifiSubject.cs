using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_CertifiSubject
    {
        public long ID { get; set; }

        [Required]
        [StringLength(100)]
        public string CertificateId { get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectId { get; set; }

        [StringLength(100)]
        public string NormalResult { get; set; }

        [StringLength(100)]
        public string ExamResult { get; set; }

        [Required]
        [StringLength(1)]
        public string IsNeedExam { get; set; }

        public long? ExamLength { get; set; }

        [Required]
        [StringLength(1)]
        public string IsDel { get; set; }

        public DateTime CreateTime { get; set; }

        public static List<Entity.MsSQL.T_CertifiSubject> GetList()
        {
            string sql = "SELECT * FROM dbo.T_CertifiSubject Where IsDel = 0 ";
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_CertifiSubject>(sql);
        }

        public static List<Entity.Respose.allcertifisubject> GetAllList()
        {
            string sql = @"SELECT dbo.T_CertifiSubject.Id,dbo.T_CertifiSubject.NormalResult,dbo.T_CertifiSubject.ExamResult,dbo.T_CertifiSubject.CertificateId,dbo.T_CertifiSubject.SubjectId,dbo.T_Subject.OLSchoolId,dbo.T_Subject.Name,dbo.T_Subject.Category,dbo.T_Subject.Price 
            FROM dbo.T_CertifiSubject left join dbo.T_Subject on dbo.T_CertifiSubject.SubjectId=dbo.T_Subject.ID WHERE dbo.T_CertifiSubject.IsDel = 0 ";
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.allcertifisubject>(sql);
        }

        public static List<Entity.Respose.allcertifisubject> GetAllListByCertId(string certid)
        {
            string sql = @"SELECT dbo.T_CertifiSubject.Id,dbo.T_CertifiSubject.NormalResult,dbo.T_CertifiSubject.ExamResult,dbo.T_CertifiSubject.CertificateId,dbo.T_CertifiSubject.SubjectId,dbo.T_Subject.OLSchoolId,dbo.T_Subject.OLSchoolAOMid,dbo.T_Subject.Name,dbo.T_Subject.Category,dbo.T_Subject.Price,dbo.T_Subject.OLAccCourseId,dbo.T_Subject.OLPaperID 
            FROM dbo.T_CertifiSubject left join dbo.T_Subject on dbo.T_CertifiSubject.SubjectId=dbo.T_Subject.ID WHERE dbo.T_CertifiSubject.IsDel = 0 ";
            string sqlCondetion = "";
            if (certid != null)
            {
                sqlCondetion += string.Format(" AND CertificateId = '{0}' ", certid);
            }
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.allcertifisubject>(sql + sqlCondetion);
        }

        public static List<Entity.Respose.subjectsbycertid> GetListByCertId(string certid, string page, string limit, ref long count)
        {
            string sql = "SELECT count(*) FROM dbo.T_CertifiSubject WHERE IsDel = 0 ";
            string sqlCondetion = "";
            if (certid != null)
            {
                sqlCondetion += string.Format(" AND CertificateId = '{0}' ", certid);
            }
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,dbo.T_CertifiSubject.Id,dbo.T_CertifiSubject.NormalResult,dbo.T_CertifiSubject.ExamResult, ", _page * _limit);
            sqltext += "dbo.T_CertifiSubject.IsNeedExam,dbo.T_CertifiSubject.ExamLength,dbo.T_CertifiSubject.SubjectId,dbo.T_Subject.Name,dbo.T_Subject.Category,dbo.T_Subject.Price ";
            sqltext += "FROM dbo.T_CertifiSubject left join dbo.T_Subject on dbo.T_CertifiSubject.SubjectId=dbo.T_Subject.ID WHERE dbo.T_CertifiSubject.IsDel = 0 ";
            sqltext += sqlCondetion;
            sqltext += "ORDER BY dbo.T_CertifiSubject.CreateTime desc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.Respose.subjectsbycertid> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.subjectsbycertid>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.Respose.subjectsbycertid>();
            }
            else
            {
                return list;
            }
        }

        public static long Add(Entity.MsSQL.T_CertifiSubject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_CertifiSubject(");
            strSql.Append("CertificateId,SubjectId,NormalResult,ExamResult,IsNeedExam,ExamLength)");
            strSql.Append(" values (");
            strSql.Append("@CertificateId,@SubjectId,@NormalResult,@ExamResult,@IsNeedExam,@ExamLength)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CertificateId", SqlDbType.VarChar,100),
					new SqlParameter("@SubjectId", SqlDbType.VarChar,100),
					new SqlParameter("@NormalResult", SqlDbType.VarChar,100),
					new SqlParameter("@ExamResult", SqlDbType.VarChar,100),
                    new SqlParameter("@IsNeedExam", SqlDbType.Char,1),
					new SqlParameter("@ExamLength", SqlDbType.BigInt)
					};
            parameters[0].Value = model.CertificateId;
            parameters[1].Value = model.SubjectId;
            parameters[2].Value = model.NormalResult;
            parameters[3].Value = model.ExamResult;
            parameters[4].Value = model.IsNeedExam;
            parameters[5].Value = model.ExamLength;
            object obj = Untity.HelperMsSQL.ExecuteScalar(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static bool Update(Entity.MsSQL.T_CertifiSubject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CertifiSubject set ");
            strSql.Append("SubjectId=@SubjectId,");
            strSql.Append("NormalResult=@NormalResult,");
            strSql.Append("ExamResult=@ExamResult,");
            strSql.Append("IsNeedExam=@IsNeedExam,");
            strSql.Append("ExamLength=@ExamLength ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@SubjectId", SqlDbType.VarChar,100),
					new SqlParameter("@NormalResult", SqlDbType.VarChar,100),
					new SqlParameter("@ExamResult", SqlDbType.VarChar,100),
                    new SqlParameter("@IsNeedExam", SqlDbType.Char,1),
					new SqlParameter("@ExamLength", SqlDbType.BigInt),
					new SqlParameter("@ID", SqlDbType.BigInt)
					};
            parameters[0].Value = model.SubjectId;
            parameters[1].Value = model.NormalResult;
            parameters[2].Value = model.ExamResult;
            parameters[3].Value = model.IsNeedExam;
            parameters[4].Value = model.ExamLength;
            parameters[5].Value = model.ID;

            int rows = Untity.HelperMsSQL.ExecuteQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Delete(string idnumber)
        {
            string sql = string.Format("UPDATE dbo.T_CertifiSubject SET IsDel = 1 WHERE ID = '{0}' ", idnumber);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

        public static long GetCertifiSubjectCount(string _ID, string _certificateid, string _subjectid)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_CertifiSubject WHERE IsDel = 0 AND CertificateId = '{0}' AND SubjectId='{1}' ", _certificateid, _subjectid);
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(_ID))
            {
                sqlCondetion += string.Format(" AND Id <> '{0}' ", _ID);
            }
            object obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        public static int GetSubjectCountBySubjectId(string subjectids)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_CertifiSubject WHERE IsDel = 0 AND SubjectId ='{0}' ", subjectids);
            object obj = Untity.HelperMsSQL.ExecuteScalar(sql);
            if (obj == null || obj.ToString() == "0")
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

    }
}
