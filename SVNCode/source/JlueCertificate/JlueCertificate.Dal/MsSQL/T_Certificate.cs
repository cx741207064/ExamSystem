using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_Certificate
    {
        public static Entity.MsSQL.T_Certificate GetModel(long Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from T_Certificate ");
            strSql.Append(string.Format(" where Id= {0}", Id));
            List<Entity.MsSQL.T_Certificate> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Certificate>(strSql.ToString());
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_Certificate> GetList()
        {
            string sql = "SELECT * FROM dbo.T_Certificate WHERE IsDel = 0 ";
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Certificate>(sql);
        }

        public static List<Entity.MsSQL.T_Certificate> GetList(string certificateids)
        {
            string sql = string.Format("SELECT * FROM dbo.T_Certificate WHERE IsDel = 0 AND Id in({0}) ", certificateids);
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Certificate>(sql);
        }

        public static List<Entity.MsSQL.T_Certificate> GetList(string name, string page, string limit, ref long count)
        {
            string sql = "SELECT count(*) FROM dbo.T_Certificate WHERE IsDel = 0 ";
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(name))
            {
                sqlCondetion += string.Format(" AND (CategoryName like '%{0}%' OR ExamSubject LIKE '%{0}%') ", name);
            }
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* ", _page * _limit);
            sqltext += "FROM dbo.T_Certificate WHERE IsDel = 0 ";
            sqltext += sqlCondetion;
            sqltext += "ORDER BY dbo.T_Certificate.CreateTime desc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_Certificate> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Certificate>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_Certificate>();
            }
            else
            {
                return list;
            }
        }

        public static List<Entity.Respose.unsignupcertificate> GetUnsignupList(string certificateids, string page, string limit, ref long count)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_Certificate WHERE IsDel = 0 ", certificateids);
            string sqlCondetion = "";
            if (certificateids.Length > 0)
            {
                sqlCondetion += string.Format(" AND Id not in({0}) ", certificateids);
            }
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* ", _page * _limit);
            sqltext += "FROM dbo.T_Certificate WHERE IsDel = 0 ";
            sqltext += sqlCondetion;
            sqltext += "ORDER BY dbo.T_Certificate.CreateTime desc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.Respose.unsignupcertificate> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.unsignupcertificate>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.Respose.unsignupcertificate>();
            }
            else
            {
                return list;
            }
        }

        public static List<Entity.Respose.signupcertificate> GetSignUpList(string studentid, string certstate, string page, string limit, ref long count)
        {
            string sql = string.Format(@"SELECT count(*) FROM dbo.T_StudentTicket" +
                " WHERE dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertState= '{1}'  AND dbo.T_StudentTicket.IsDel = 0 ", studentid, certstate);

            var obj = Untity.HelperMsSQL.ExecuteScalar(sql);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,dbo.T_StudentTicket.Id,dbo.T_StudentTicket.TicketNum,dbo.T_Certificate.CategoryName,dbo.T_Certificate.ExamSubject,dbo.T_Certificate.StartTime,dbo.T_Certificate.EndTime ", _page * _limit);
            sqltext += "FROM dbo.T_StudentTicket LEFT JOIN dbo.T_Certificate on dbo.T_StudentTicket.CertificateId = dbo.T_Certificate.Id ";
            sqltext += string.Format("WHERE dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertState= '{1}'  AND dbo.T_StudentTicket.IsDel = 0 ", studentid, certstate);
            sqltext += "ORDER BY dbo.T_StudentTicket.Id asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.Respose.signupcertificate> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.signupcertificate>(sqltext);

            if (list == null || list.Count == 0)
            {
                return new List<Entity.Respose.signupcertificate>();
            }
            else
            {
                return list;
            }
        }

        public static List<Entity.Respose.holdcertificate> GetHoldList(string studentid, string certstate, string page, string limit, ref long count)
        {
            string sql = string.Format(@"SELECT count(*) FROM dbo.T_StudentTicket" +
                " WHERE dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertState= '{1}'  AND dbo.T_StudentTicket.IsDel = 0 ", studentid, certstate);
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,dbo.T_StudentTicket.Id,dbo.T_CertifiSerial.SerialNum,dbo.T_Certificate.CategoryName,dbo.T_Certificate.ExamSubject,dbo.T_CertifiSerial.CreateTime,dbo.T_StudentTicket.CertState ", _page * _limit);
            sqltext += "FROM dbo.T_StudentTicket LEFT JOIN dbo.T_CertifiSerial on dbo.T_StudentTicket.Id = dbo.T_CertifiSerial.TicketId ";
            sqltext += "LEFT JOIN dbo.T_Certificate on dbo.T_StudentTicket.CertificateId = dbo.T_Certificate.Id ";
            sqltext += string.Format("WHERE dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertState= '{1}'  AND dbo.T_StudentTicket.IsDel = 0 ", studentid, certstate);
            sqltext += "ORDER BY dbo.T_StudentTicket.Id asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.Respose.holdcertificate> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.holdcertificate>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.Respose.holdcertificate>();
            }
            else
            {
                return list;
            }
        }

        public static long Add(Entity.MsSQL.T_Certificate model)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Certificate(");
            strSql.Append("CategoryName,ExamSubject,StartTime,EndTime,NormalResult,ExamResult,[Rule],Describe)");
            strSql.Append(" values (");
            strSql.Append("@CategoryName,@ExamSubject,@StartTime,@EndTime,@NormalResult,@ExamResult,@Rule,@Describe)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,200),
					new SqlParameter("@ExamSubject", SqlDbType.NVarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@NormalResult", SqlDbType.VarChar,100),
					new SqlParameter("@ExamResult", SqlDbType.VarChar,100),
					new SqlParameter("@Rule", SqlDbType.Char,1),
					new SqlParameter("@Describe", SqlDbType.NVarChar,2000)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.ExamSubject;
            parameters[2].Value = model.StartTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.NormalResult;
            parameters[5].Value = model.ExamResult;
            parameters[6].Value = model.Rule;
            parameters[7].Value = model.Describe;

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

        public static bool Update(Entity.MsSQL.T_Certificate model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Certificate set ");
            strSql.Append("CategoryName=@CategoryName,");
            strSql.Append("ExamSubject=@ExamSubject,");
            strSql.Append("StartTime=@StartTime,");
            strSql.Append("EndTime=@EndTime,");
            strSql.Append("NormalResult=@NormalResult,");
            strSql.Append("ExamResult=@ExamResult,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("IsDel=@IsDel ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@CategoryName", SqlDbType.NVarChar,200),
					new SqlParameter("@ExamSubject", SqlDbType.NVarChar,200),
					new SqlParameter("@StartTime", SqlDbType.DateTime),
					new SqlParameter("@EndTime", SqlDbType.DateTime),
					new SqlParameter("@NormalResult", SqlDbType.VarChar,100),
					new SqlParameter("@ExamResult", SqlDbType.VarChar,100),
					new SqlParameter("@Describe", SqlDbType.NVarChar,2000),
					new SqlParameter("@IsDel", SqlDbType.Char,1),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.CategoryName;
            parameters[1].Value = model.ExamSubject;
            parameters[2].Value = model.StartTime;
            parameters[3].Value = model.EndTime;
            parameters[4].Value = model.NormalResult;
            parameters[5].Value = model.ExamResult;
            parameters[6].Value = model.Describe;
            parameters[7].Value = model.IsDel;
            parameters[8].Value = model.Id;

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
            string sql = string.Format("UPDATE dbo.T_Certificate SET IsDel = 1 WHERE Id = '{0}' ", idnumber);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }
    }
}
