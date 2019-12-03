using JlueCertificate.Untity;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_ExamSeat
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int id { get; set; }
        public string ExamRoomId { get; set; }
        public string SeatNumber { get; set; }
        public string TicketId { get; set; }
        //获取考场座位
        public static List<Entity.Respose.getexamseatInfo> GetExamseatInfoModel(string ExamRoomId)
        {
            string sql = string.Format("SELECT id, ExamRoomId , SeatNumber,TicketId" +
                                        " FROM T_ExamSeat where ExamRoomId = '{0}'", ExamRoomId);
            List<Entity.Respose.getexamseatInfo> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getexamseatInfo>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list;
            }
        }
        public static bool isSelect(Entity.MsSQL.T_ExamSeat model)
        {
            bool flag = false;
            string sql = string.Format("SELECT id, ExamRoomId , SeatNumber,TicketId" +
                                        " FROM T_ExamSeat where ExamRoomId = '{0}' AND SeatNumber = '{1}'", model.ExamRoomId,model.SeatNumber);
            List<Entity.Respose.getexamseatInfo> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getexamseatInfo>(sql);
            if (list == null || list.Count == 0)
            {
                return flag;
            }
            else
            {
                Entity.Respose.getexamseatInfo result = list.FirstOrDefault();
                if (result.TicketId!="") {
                    flag = true;
                    return flag;
                }
                    return flag;
            }
        }
        //判断座位是否绑定
        public static bool isSelectSeat(string _ticketnum)
        {
            bool flag = false;
            string sql = string.Format("SELECT id, ExamRoomId , SeatNumber,TicketId" +
                                        " FROM T_ExamSeat where TicketId = '{0}'", _ticketnum);
            List<Entity.Respose.getexamseatInfo> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getexamseatInfo>(sql);
            if (list == null || list.Count == 0)
            {
                return flag;
            }
            else
            {
                    flag = true;
                    return flag;
            }
        }

        //查询考场座位
        public static bool Select(Entity.MsSQL.T_ExamSeat model)
        {
            string sql = string.Format("select count(*) from T_ExamSeat  where ExamRoomId='{0}' AND SeatNumber='{1}'", model.ExamRoomId, model.SeatNumber);
            bool flag = Untity.HelperMsSQL.ExecuteQuerySelect(sql);
            return flag;
        }
        //考场座位添加
        public static long Add(Entity.MsSQL.T_ExamSeat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ExamSeat(");
            strSql.Append("ExamRoomId,SeatNumber,TicketId)");
            strSql.Append(" values (");
            strSql.Append("@ExamRoomId,@SeatNumber,@TicketId)");
            SqlParameter[] parameters = {
                    new SqlParameter("@ExamRoomId", SqlDbType.VarChar,50),
                    new SqlParameter("@SeatNumber", SqlDbType.VarChar,50),
                    new SqlParameter("@TicketId", SqlDbType.VarChar,50)};
            parameters[0].Value = model.ExamRoomId;
            parameters[1].Value = model.SeatNumber;
            parameters[2].Value = model.TicketId;
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
        //（座位添加准考证）
        public static bool Update(Entity.MsSQL.T_ExamSeat model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ExamSeat set ");
            strSql.Append("TicketId=@TicketId");
            strSql.Append(" where ExamRoomId=@ExamRoomId AND SeatNumber=@SeatNumber ");
            SqlParameter[] parameters = {
                    new SqlParameter("@TicketId", SqlDbType.VarChar,50),
                    new SqlParameter("@ExamRoomId", SqlDbType.VarChar,50),
                    new SqlParameter("@SeatNumber", SqlDbType.VarChar,50)
            };
            parameters[0].Value = model.TicketId;
            parameters[1].Value = model.ExamRoomId;
            parameters[2].Value = model.SeatNumber;
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
    }
}
