using JlueCertificate.Untity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_ExamRoom
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }
        public string ExamName { get; set; }
        public string ExamPlace { get; set; }
        public string CentreName { get; set; }
        public string ExamNum { get; set; }
        public string SeatNum { get; set; }
        public DateTime ResultReleaseTime { get; set; }
        public DateTime createtime { get; set; }
        public string IsDel { get; set; }
        //获取考场
        public static List<Entity.Respose.getexamInfo> GetExamInfoModel()
        {
            string sql = string.Format("SELECT id, ExamName , ExamPlace,CentreName,ExamNum" +
                                        " FROM T_ExamRoom where IsDel = 0");
            List<Entity.Respose.getexamInfo> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getexamInfo>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list;
            }
        }

        //添加考场
        public static long Add(Entity.MsSQL.T_ExamRoom model,string postString)
        {
            string sql = string.Format("insert into T_ExamRoom(ExamName,ExamPlace,CentreName,ExamNum,SeatNum,ResultReleaseTime,createtime,IsDel)values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');select @@identity", model.ExamName, model.ExamPlace, model.CentreName, model.ExamNum, model.SeatNum, model.ResultReleaseTime, model.CreateTime, model.IsDel);
            string ExamRoomId = Untity.HelperMsSQL.ExecuteQueryExam(sql).ToString();
            if (ExamRoomId != "" && ExamRoomId != null)
            {
                JArray Seats = JArray.Parse(JObject.Parse(postString)["detal"].ToString());
                StringBuilder SeatSQL = new StringBuilder();
                SeatSQL.Append("insert into T_ExamSeat(");
                SeatSQL.Append("ExamRoomId,SeatNumber,TicketId)");
                SeatSQL.Append(" values (");
                SeatSQL.Append("@ExamRoomId,@SeatNumber,@TicketId)");
                for (int i = 0; i < Seats.Count; i++)
                {
                    SqlParameter[] parametersSeat = {
                    new SqlParameter("@ExamRoomId", SqlDbType.VarChar,50),
                    new SqlParameter("@SeatNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@TicketId", SqlDbType.VarChar,50)};
                    parametersSeat[0].Value = ExamRoomId;
                    parametersSeat[1].Value = Seats[i]["SeatNumber"];
                    parametersSeat[2].Value = "";
                    object objSeat = Untity.HelperMsSQL.ExecuteScalar(SeatSQL.ToString(), parametersSeat);
                    if (objSeat != null)
                    {
                        return Convert.ToInt64(objSeat);
                    }
                }
                return 0;
            }
            else {
                return Convert.ToInt64(-1);
            }
        }
        //修改考场
        public static bool Update(Entity.MsSQL.T_ExamRoom model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ExamRoom set ");
            strSql.Append("ExamName=@ExamName,");
            strSql.Append("ExamPlace=@ExamPlace,");
            strSql.Append("CentreName=@CentreName,");
            strSql.Append("ExamNum=@ExamNum,");
            strSql.Append("SeatNum=@SeatNum,");
            strSql.Append("ResultReleaseTime=@ResultReleaseTime,");
            strSql.Append("createtime=@createtime,");
            strSql.Append("IsDel=@IsDel");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ExamName", SqlDbType.VarChar,50),
                    new SqlParameter("@ExamPlace", SqlDbType.VarChar,50),
                    new SqlParameter("@CentreName", SqlDbType.VarChar,50),
                    new SqlParameter("@ExamNum", SqlDbType.VarChar,50),
                    new SqlParameter("@SeatNum", SqlDbType.VarChar,50),
                    new SqlParameter("@ResultReleaseTime", SqlDbType.DateTime),
                    new SqlParameter("@createtime", SqlDbType.DateTime),
                    new SqlParameter("@IsDel", SqlDbType.Char,1),
                    new SqlParameter("@Id", SqlDbType.Int,100)
            };
            parameters[0].Value = model.ExamName;
            parameters[1].Value = model.ExamPlace;
            parameters[2].Value = model.CentreName;
            parameters[3].Value = model.ExamNum;
            parameters[4].Value = model.SeatNum;
            parameters[5].Value = model.ResultReleaseTime;
            parameters[6].Value = model.CreateTime;
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
        //删除考场
        public static void Delete(string id)
        {
            string sql = string.Format("UPDATE dbo.T_ExamRoom SET IsDel = 1 WHERE Id = '{0}' ", id);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }
        public static List<Entity.MsSQL.T_ExamRoom> GetRomByid(string id)
        {
            string sqltext = "select * from T_ExamRoom where id='" + id + "'";

            List<Entity.MsSQL.T_ExamRoom> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_ExamRoom>(sqltext);
            return list;
        }
        public static List<Entity.MsSQL.T_ExamRoom> GetListByPage(string _name, string page, string limit, ref long count)
        {
            string sql = "SELECT COUNT(*) FROM dbo.T_ExamRoom Where IsDel = 0 ";
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(_name))
            {
                sqlCondetion += string.Format(" AND (ExamName like '%{0}%' OR CentreName LIKE '%{0}%') ", _name);
            }

            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,* FROM dbo.T_ExamRoom  Where IsDel = 0 ", _page * _limit);
            sqltext += sqlCondetion;
            sqltext += " ORDER BY CreateTime asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.MsSQL.T_ExamRoom> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_ExamRoom>(sqltext);

            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_ExamRoom>();
            }
            else
            {
                return list;
            }
        }
    }
}
