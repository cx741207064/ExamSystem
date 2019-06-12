using JlueCertificate.Untity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public static class T_Student
    {
        public static Entity.MsSQL.T_Student GetModel(string _name, string _cardid)
        {
            string sql = "SELECT * FROM dbo.T_Student WHERE 1=1 ";
            string sqlcon = string.Empty;
            if (!string.IsNullOrEmpty(_name))
            {
                sqlcon += string.Format("AND Name ='{0}' ", _name);
            }
            if (!string.IsNullOrEmpty(_name))
            {
                sqlcon += string.Format("AND CardId ='{0}'", _cardid);
            }
            List<Entity.MsSQL.T_Student> list = new List<Entity.MsSQL.T_Student>();
            if (!string.IsNullOrEmpty(sqlcon))
            {
                list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Student>(sql + sqlcon);
            }
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }
        
        public static Entity.MsSQL.T_Student GetModelByCardId(string _cardid)
        {
            string sql = string.Format("SELECT * FROM dbo.T_Student WHERE CardId = '{0}' ", _cardid);
            List<Entity.MsSQL.T_Student> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Student>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static Entity.MsSQL.T_Student GetModel(string studentid)
        {
            string sql = string.Format("SELECT * FROM dbo.T_Student WHERE Id = {0} ", studentid);
            List<Entity.MsSQL.T_Student> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Student>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_Student> GetListByPage(long _orgaid, string _name, string _cardid, string page, string limit, ref long count)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Student Where IsDel = 0 ";
            string sqlCondetion = "";
            if (_orgaid > 0)
            {
                sqlCondetion += string.Format(" And OrgaId = {0} ", _orgaid);
            }
            if (!string.IsNullOrEmpty(_name))
            {
                sqlCondetion += string.Format(" And Name LIKE '%{0}%' ", _name);
            }
            if (!string.IsNullOrEmpty(_cardid))
            {
                sqlCondetion += string.Format(" And Cardid LIKE '%{0}%' ", _cardid);
            }

            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* FROM dbo.T_Student  Where IsDel = 0 ", _page * _limit);
            sqltext += sqlCondetion;
            sqltext += " ORDER BY CreateTime desc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_Student> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Student>(sqltext);

            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_Student>();
            }
            else
            {
                return list;
            }
        }

        public static long Add(Entity.MsSQL.T_Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Student(");
            strSql.Append("Id,OrgaId,Name,CardId,HeaderUrl,Sex,TelPhone,ProvinceId,CityId,ZoneId,Address,PostProvinceId,PostCityId,PostZoneId,PostAddress,IsDel,CreateTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@OrgaId,@Name,@CardId,@HeaderUrl,@Sex,@TelPhone,@ProvinceId,@CityId,@ZoneId,@Address,@PostProvinceId,@PostCityId,@PostZoneId,@PostAddress,@IsDel,@CreateTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.VarChar,100),
					new SqlParameter("@OrgaId", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@CardId", SqlDbType.VarChar,50),
					new SqlParameter("@HeaderUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Sex", SqlDbType.Char,1),
					new SqlParameter("@TelPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ProvinceId", SqlDbType.VarChar,100),
					new SqlParameter("@CityId", SqlDbType.VarChar,100),
					new SqlParameter("@ZoneId", SqlDbType.VarChar,100),
					new SqlParameter("@Address", SqlDbType.VarChar,100),
					new SqlParameter("@PostProvinceId", SqlDbType.VarChar,100),
					new SqlParameter("@PostCityId", SqlDbType.VarChar,100),
					new SqlParameter("@PostZoneId", SqlDbType.VarChar,100),
					new SqlParameter("@PostAddress", SqlDbType.VarChar,100),
					new SqlParameter("@IsDel", SqlDbType.Char,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.OrgaId;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.CardId;
            parameters[4].Value = model.HeaderUrl;
            parameters[5].Value = model.Sex;
            parameters[6].Value = model.TelPhone;
            parameters[7].Value = model.ProvinceId;
            parameters[8].Value = model.CityId;
            parameters[9].Value = model.ZoneId;
            parameters[10].Value = model.Address;
            parameters[11].Value = model.PostProvinceId;
            parameters[12].Value = model.PostCityId;
            parameters[13].Value = model.PostZoneId;
            parameters[14].Value = model.PostAddress;
            parameters[15].Value = model.IsDel;
            parameters[16].Value = model.CreateTime;

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

        public static bool Update(Entity.MsSQL.T_Student model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Student set ");
            strSql.Append("OrgaId=@OrgaId,");
            strSql.Append("Name=@Name,");
            strSql.Append("CardId=@CardId,");
            strSql.Append("HeaderUrl=@HeaderUrl,");
            strSql.Append("Sex=@Sex,");
            strSql.Append("TelPhone=@TelPhone,");
            strSql.Append("ProvinceId=@ProvinceId,");
            strSql.Append("CityId=@CityId,");
            strSql.Append("ZoneId=@ZoneId,");
            strSql.Append("Address=@Address,");
            strSql.Append("PostProvinceId=@PostProvinceId,");
            strSql.Append("PostCityId=@PostCityId,");
            strSql.Append("PostZoneId=@PostZoneId,");
            strSql.Append("PostAddress=@PostAddress,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("CreateTime=@CreateTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrgaId", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@CardId", SqlDbType.VarChar,50),
					new SqlParameter("@HeaderUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@Sex", SqlDbType.Char,1),
					new SqlParameter("@TelPhone", SqlDbType.VarChar,50),
					new SqlParameter("@ProvinceId", SqlDbType.VarChar,100),
					new SqlParameter("@CityId", SqlDbType.VarChar,100),
					new SqlParameter("@ZoneId", SqlDbType.VarChar,100),
					new SqlParameter("@Address", SqlDbType.VarChar,100),
					new SqlParameter("@PostProvinceId", SqlDbType.VarChar,100),
					new SqlParameter("@PostCityId", SqlDbType.VarChar,100),
					new SqlParameter("@PostZoneId", SqlDbType.VarChar,100),
					new SqlParameter("@PostAddress", SqlDbType.VarChar,100),
					new SqlParameter("@IsDel", SqlDbType.Char,1),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.VarChar,100)};
            parameters[0].Value = model.OrgaId;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.CardId;
            parameters[3].Value = model.HeaderUrl;
            parameters[4].Value = model.Sex;
            parameters[5].Value = model.TelPhone;
            parameters[6].Value = model.ProvinceId;
            parameters[7].Value = model.CityId;
            parameters[8].Value = model.ZoneId;
            parameters[9].Value = model.Address;
            parameters[10].Value = model.PostProvinceId;
            parameters[11].Value = model.PostCityId;
            parameters[12].Value = model.PostZoneId;
            parameters[13].Value = model.PostAddress;
            parameters[14].Value = model.IsDel;
            parameters[15].Value = model.CreateTime;
            parameters[16].Value = model.Id;

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
            string sql = string.Format("UPDATE dbo.T_Student SET IsDel = 1 WHERE Id = '{0}' ", idnumber);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

        public static Entity.Respose.OLUserResponse getOLSchoolUserId(Entity.MsSQL.T_Organiza _orga, string _username, string _password)
        {
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = HelperAppSet.getAppSetting("olschoolpath");
            string fullpath = path + "/Member/GetUserId?classid=" + _orga.ClassId + "&UserName=" + _username + "&PWD=" + _password;
            string json = p.Get(fullpath);
            Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            Entity.Respose.OLUserResponse rep = Untity.HelperJson.DeserializeObject<Entity.Respose.OLUserResponse>(Untity.HelperDataCvt.objToString(result.Data));
            return rep;
        }

        public static void updateOLSchoolUserId(string _OLSchoolUserId, string _OLSchoolUserName, string _OLSchoolPWD, string _ID)
        {
            string sql = string.Format("UPDATE dbo.T_Student SET OLSchoolUserId = '{0}',OLSchoolUserName='{1}',OLSchoolPWD='{2}' WHERE Id = '{3}' ", _OLSchoolUserId, _OLSchoolUserName, _OLSchoolPWD, _ID);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

        public static long GetOLSchoolUserCount(string _ID, string _OLSchoolUserName, string _OrgaId)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_Student WHERE IsDel = 0 AND dbo.T_Student.ID <> '{0}' AND dbo.T_Student.OLSchoolUserName='{1}' AND dbo.T_Student.OrgaId='{2}' ", _ID, _OLSchoolUserName, _OrgaId);
            object obj = Untity.HelperMsSQL.ExecuteScalar(sql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
    }
}
