using JlueCertificate.Untity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_Subject
    {

        public static Entity.MsSQL.T_Subject GetModel(long _subjectid)
        {
            string sql = string.Format("SELECT * FROM dbo.T_Subject WHERE IsDel = 0 AND ID = {0} ", _subjectid);
            List<Entity.MsSQL.T_Subject> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Subject>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_Subject> GetList(string Ids)
        {
            if (string.IsNullOrEmpty(Ids))
            {
                return new List<Entity.MsSQL.T_Subject>();
            }
            string sql = string.Format("SELECT * FROM dbo.T_Subject WHERE IsDel = 0 AND ID IN ({0}) ", Ids);
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Subject>(sql);
        }


        public static List<Entity.MsSQL.T_Subject> GetAllList()
        {
            string sql = string.Format("SELECT * FROM dbo.T_Subject WHERE IsDel = 0 ");
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Subject>(sql);
        }


        public static List<Entity.MsSQL.T_Subject> GetList(string name, string page, string limit, ref long count)
        {
            string sql = "SELECT count(*) FROM dbo.T_Subject WHERE IsDel = 0 ";
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(name))
            {
                sqlCondetion += string.Format(" AND Name like '%{0}%' ", name);
            }
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* ", _page * _limit);
            sqltext += "FROM dbo.T_Subject WHERE IsDel = 0 ";
            sqltext += sqlCondetion;
            sqltext += "ORDER BY dbo.T_Subject.CreateTime desc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_Subject> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Subject>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_Subject>();
            }
            else
            {
                return list;
            }
        }

        public static List<Entity.Respose.olschoolsubject> GetOLSchoolAllList()
        {
            HelperMethod p = new HelperMethod();
            string path = HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Member/GetCourse";
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            return Untity.HelperJson.DeserializeObject<List<Entity.Respose.olschoolsubject>>(HelperDataCvt.objToString(result.Data));
        }

        public static bool IsBuyAll(Entity.MsSQL.T_Organiza _orga, string _ids, string _username, ref string error)
        {
            HelperMethod p = new HelperMethod();
            //string path = HelperAppSet.getAppSetting("olschoolpath");
            //string classid = HelperAppSet.getAppSetting("classid");
            string path = HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Member/IsBuyAll?classid=" + _orga.ClassId + "&UserName=" + _username + "&Ids=" + _ids;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            if (HelperDataCvt.objToString(result.Data).Contains("未购买"))
            {
                error = HelperDataCvt.objToString(result.Data);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static long Add(Entity.MsSQL.T_Subject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Subject(");
            strSql.Append("Name,Category,Price,Describe,OLSchoolId,OLSchoolName,OLSchoolProvinceId,OLSchoolCourseId,OLSchoolQuestionNum,OLSchoolAOMid,OLSchoolMasterTypeId,OLAccCourseId)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Category,@Price,@Describe,@OLSchoolId,@OLSchoolName,@OLSchoolProvinceId,@OLSchoolCourseId,@OLSchoolQuestionNum,@OLSchoolAOMid,@OLSchoolMasterTypeId,@OLAccCourseId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Category", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.VarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
					new SqlParameter("@OLSchoolId", SqlDbType.NVarChar,100),
					new SqlParameter("@OLSchoolName", SqlDbType.NVarChar,200),
					new SqlParameter("@OLSchoolProvinceId", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolCourseId", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolQuestionNum", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolAOMid", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolMasterTypeId", SqlDbType.VarChar,10),
                    new SqlParameter("@OLAccCourseId", SqlDbType.VarChar,10)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Category;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.OLSchoolId;
            parameters[5].Value = model.OLSchoolName;
            parameters[6].Value = model.OLSchoolProvinceId;
            parameters[7].Value = model.OLSchoolCourseId;
            parameters[8].Value = model.OLSchoolQuestionNum;
            parameters[9].Value = model.OLSchoolAOMid;
            parameters[10].Value = model.OLSchoolMasterTypeId;
            parameters[11].Value = model.OLAccCourseId;

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

        public static bool Update(Entity.MsSQL.T_Subject model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Subject set ");
            strSql.Append("Name=@Name,");
            strSql.Append("Category=@Category,");
            strSql.Append("Price=@Price,");
            strSql.Append("Describe=@Describe,");
            strSql.Append("OLSchoolId=@OLSchoolId,");
            strSql.Append("OLSchoolName=@OLSchoolName,");
            strSql.Append("OLSchoolProvinceId=@OLSchoolProvinceId,");
            strSql.Append("OLSchoolCourseId=@OLSchoolCourseId,");
            strSql.Append("OLSchoolQuestionNum=@OLSchoolQuestionNum,");
            strSql.Append("OLSchoolAOMid=@OLSchoolAOMid,");
            strSql.Append("OLSchoolMasterTypeId=@OLSchoolMasterTypeId,");
            strSql.Append("OLAccCourseId=@OLAccCourseId,");
            strSql.Append("IsDel=@IsDel ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Category", SqlDbType.NVarChar,200),
					new SqlParameter("@Price", SqlDbType.VarChar,50),
					new SqlParameter("@Describe", SqlDbType.NVarChar,500),
					new SqlParameter("@OLSchoolId", SqlDbType.NVarChar,100),
					new SqlParameter("@OLSchoolName", SqlDbType.NVarChar,200),
					new SqlParameter("@OLSchoolProvinceId", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolCourseId", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolQuestionNum", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolAOMid", SqlDbType.VarChar,10),
					new SqlParameter("@OLSchoolMasterTypeId", SqlDbType.VarChar,10),
					new SqlParameter("@OLAccCourseId", SqlDbType.VarChar,10),
					new SqlParameter("@IsDel", SqlDbType.Char,1),
					new SqlParameter("@ID", SqlDbType.BigInt,8)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Category;
            parameters[2].Value = model.Price;
            parameters[3].Value = model.Describe;
            parameters[4].Value = model.OLSchoolId;
            parameters[5].Value = model.OLSchoolName;
            parameters[6].Value = model.OLSchoolProvinceId;
            parameters[7].Value = model.OLSchoolCourseId;
            parameters[8].Value = model.OLSchoolQuestionNum;
            parameters[9].Value = model.OLSchoolAOMid;
            parameters[10].Value = model.OLSchoolMasterTypeId;
            parameters[11].Value = model.OLAccCourseId;
            parameters[12].Value = 0;
            parameters[13].Value = model.ID;

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
            string sql = string.Format("UPDATE dbo.T_Subject SET IsDel = 1 WHERE ID = '{0}' ", idnumber);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

    }
}
