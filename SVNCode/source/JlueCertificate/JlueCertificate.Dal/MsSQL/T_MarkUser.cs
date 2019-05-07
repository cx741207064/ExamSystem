using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_MarkUser
    {
        public static Entity.MsSQL.T_MarkUser GetModel(string _name, string _password)
        {
            string sql = string.Format("select * from T_MarkUser where Name = '{0}' AND Password='{1}' AND IsDel = 0 ", _name, _password);
            List<Entity.MsSQL.T_MarkUser> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_MarkUser>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_MarkUser> GetListByPage(string _name, string page, string limit, ref long count)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_MarkUser Where IsDel = 0 ";
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(_name))
            {
                sqlCondetion += string.Format(" And Name LIKE '%{0}%' ", _name);
            }

            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* FROM dbo.T_MarkUser  Where IsDel = 0 ", _page * _limit);
            sqltext += sqlCondetion;
            sqltext += " ORDER BY CreateTime asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_MarkUser> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_MarkUser>(sqltext);

            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_MarkUser>();
            }
            else
            {
                return list;
            }
        }

        public static object Add(Entity.MsSQL.T_MarkUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_MarkUser(");
            strSql.Append("Name,Password,Level)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Password,@Level)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@Level", SqlDbType.Char,1)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.Level;

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

        public static object Update(Entity.MsSQL.T_MarkUser model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_MarkUser set ");
            strSql.Append("Name=@Name,");
            //strSql.Append("Password=@Password,");
            strSql.Append("Level=@Level ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,200),
					//new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@Level", SqlDbType.Char,1),
					new SqlParameter("@Id", SqlDbType.BigInt,8)};
            parameters[0].Value = model.Name;
            //parameters[1].Value = model.Password;
            parameters[1].Value = model.Level;
            parameters[2].Value = model.Id;

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

        public static void Delete(string id)
        {
            string sql = string.Format("UPDATE dbo.T_MarkUser SET IsDel = 1 WHERE Id = '{0}' ", id);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

        public static Entity.MsSQL.T_MarkUser GetModel(long _id)
        {
            string sql = string.Format("select * from T_MarkUser where id = {0} ", _id);
            List<Entity.MsSQL.T_MarkUser> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_MarkUser>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static void UpdatePwd(long _id, string _password)
        {
            string sql = string.Format("UPDATE dbo.T_MarkUser SET Password = '{1}'  WHERE Id = '{0}' ", _id,_password);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }
    }
}
