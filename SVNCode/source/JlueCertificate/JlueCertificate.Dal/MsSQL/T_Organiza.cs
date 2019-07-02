using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_Organiza
    {
        public long Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string AppName { get; set; }

        [Required]
        [StringLength(10)]
        public string ClassId { get; set; }

        [Required]
        [StringLength(200)]
        public string Path { get; set; }

        [Required]
        [StringLength(2000)]
        public string Describe { get; set; }

        public int IsDel { get; set; }

        public DateTime CreateTime { get; set; }

        public static Entity.MsSQL.T_Organiza GetModel(long orgaId)
        {
            string sql = string.Format("select  * from T_Organiza where Id = {0} ", orgaId);
            List<Entity.MsSQL.T_Organiza> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Organiza>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static Entity.MsSQL.T_Organiza GetModel(string _name, string _password)
        {
            string sql = string.Format("select  * from T_Organiza where Name = '{0}' AND Password='{1}' ", _name, _password);
            List<Entity.MsSQL.T_Organiza> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Organiza>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_Organiza> GetListByPage(string _name, string page, string limit, ref long count)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_Organiza Where IsDel = 0 ";
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
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* FROM dbo.T_Organiza  Where IsDel = 0 ", _page * _limit);
            sqltext += sqlCondetion;
            sqltext += " ORDER BY CreateTime asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_Organiza> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_Organiza>(sqltext);

            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_Organiza>();
            }
            else
            {
                return list;
            }
        }

        public static object Add(Entity.MsSQL.T_Organiza model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Organiza(");
            strSql.Append("Name,Password,AppName,ClassId,Path,Describe)");
            strSql.Append(" values (");
            strSql.Append("@Name,@Password,@AppName,@ClassId,@Path,@Describe)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,200),
					new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@AppName", SqlDbType.VarChar,200),
                    new SqlParameter("@ClassId", SqlDbType.NVarChar,10),
					new SqlParameter("@Path", SqlDbType.VarChar,200),
					new SqlParameter("@Describe", SqlDbType.VarChar,2000)};
            parameters[0].Value = model.Name;
            parameters[1].Value = model.Password;
            parameters[2].Value = model.AppName;
            parameters[3].Value = model.ClassId;
            parameters[4].Value = model.Path;
            parameters[5].Value = model.Describe;

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

        public static object Update(Entity.MsSQL.T_Organiza model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Organiza set ");
            strSql.Append("Name=@Name,");
            //strSql.Append("Password=@Password,");
            strSql.Append("AppName=@AppName,");
            strSql.Append("ClassId=@ClassId,");
            strSql.Append("Path=@Path,");
            strSql.Append("Describe=@Describe ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.VarChar,200),
					//new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@AppName", SqlDbType.VarChar,200),
                    new SqlParameter("@ClassId", SqlDbType.NVarChar,10),
					new SqlParameter("@Path", SqlDbType.VarChar,200),
					new SqlParameter("@Describe", SqlDbType.VarChar,2000),
                    new SqlParameter("@Id", SqlDbType.BigInt)};
            parameters[0].Value = model.Name;
            //parameters[1].Value = model.Password;
            parameters[1].Value = model.AppName;
            parameters[2].Value = model.ClassId;
            parameters[3].Value = model.Path;
            parameters[4].Value = model.Describe;
            parameters[5].Value = model.Id;

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
            string sql = string.Format("UPDATE dbo.T_Organiza SET IsDel = 1 WHERE Id = '{0}' ", id);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }
        
        public static void UpdatePwd(long _id, string PWD)
        {
            string sql = string.Format("UPDATE dbo.T_Organiza SET Password = '{1}' WHERE Id = '{0}' ", _id, PWD);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }
    }
}
