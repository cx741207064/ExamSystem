using JlueCertificate.Dal.Settings;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_StudentTicket
    {
        public int Id { get; set; }

        public string CertificateId { get; set; }

        public int OrgaizId { get; set; }

        public string StudentId { get; set; }

        public string TicketNum { get; set; }

        public string SerialNum { get; set; }

        public int OLMobile { get; set; }

        public string IsDel { get; set; }

        public string CreateTime { get; set; }

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

        public static Entity.MsSQL.T_StudentTicket GetModel(string _ticketid)
        {
            string sql = "SELECT * FROM dbo.T_StudentTicket WHERE 1=1 ";
            if (!string.IsNullOrEmpty(_ticketid))
            {
                sql += string.Format("AND Id ='{0}' ", _ticketid);
            }
            List<Entity.MsSQL.T_StudentTicket> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_StudentTicket>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static List<Entity.MsSQL.T_StudentTicket> GetList(string studentid)
        {
            string sql = string.Format("SELECT * FROM dbo.T_StudentTicket WHERE IsDel = 0 AND dbo.T_StudentTicket.StudentId = '{0}' ", studentid);
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_StudentTicket>(sql);
        }

        public static List<Entity.Respose.ticket> GetListByTicketNum(long _orgaizid, string _ticketnum, string page, string limit, ref long count)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_StudentTicket " +
                "WHERE dbo.T_StudentTicket.IsDel = 0 AND dbo.T_StudentTicket.OrgaizId={0} ", _orgaizid);
            string sqlCondetion = "";
            if (!string.IsNullOrEmpty(_ticketnum))
            {
                sqlCondetion += string.Format("And dbo.T_StudentTicket.TicketNum like '%{0}%' ", _ticketnum);
            }
            var obj = Untity.HelperMsSQL.ExecuteScalar(sql + sqlCondetion);
            count = Untity.HelperDataCvt.strToLong(Untity.HelperDataCvt.objToString(obj), 0);
            long _page = Untity.HelperDataCvt.strToLong(page, 1);
            long _limit = Untity.HelperDataCvt.strToLong(limit, 10);
            string sqltext = "select * from ( SELECT ROW_NUMBER()OVER(ORDER BY tempcolumn) temprownumber,* FROM ";
            sqltext += string.Format("(SELECT TOP {0} tempcolumn = 0,dbo.T_StudentTicket.Id,dbo.T_StudentTicket.TicketNum,dbo.T_StudentTicket.SerialNum,dbo.T_Certificate.CategoryName,dbo.T_Certificate.ExamSubject,dbo.T_Student.Name,dbo.T_Student.OLSchoolUserId,dbo.T_Student.OLSchoolUserName,dbo.T_StudentTicket.CreateTime ", _page * _limit);
            sqltext += "FROM dbo.T_StudentTicket LEFT JOIN dbo.T_Certificate on dbo.T_StudentTicket.CertificateId = dbo.T_Certificate.Id ";
            sqltext += "LEFT JOIN dbo.T_Student on dbo.T_StudentTicket.StudentId = dbo.T_Student.Id ";
            sqltext += string.Format("WHERE dbo.T_StudentTicket.IsDel = 0 AND dbo.T_StudentTicket.OrgaizId={0} ", _orgaizid);
            sqltext += sqlCondetion;
            sqltext += "ORDER BY dbo.T_StudentTicket.Id asc) t  ";
            sqltext += string.Format(") tt where temprownumber>{0} ORDER BY tt.temprownumber asc ", (_page - 1) * _limit);
            List<Entity.Respose.ticket> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.ticket>(sqltext);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.Respose.ticket>();
            }
            else
            {
                return list;
            }
        }

        public static List<Entity.Respose.scoredetail> GetScoreDetailFromOLSchool(string _OLSchoolUserId, string _subjectids)
        {
            //Untity.HelperMethod p = new Untity.HelperMethod();
            //string path = Untity.HelperAppSet.getAppSetting("olschoolpath");
            //string classid = Untity.HelperAppSet.getAppSetting("classid");
            //string fullpath = path + "/Member/GetUserId?classid=" + classid + "&OLSchoolUserId=" + _OLSchoolUserId + "&_subjectids=" + _subjectids;
            //string json = p.Get(fullpath);
            //Entity.Respose.GTXResult result = Untity.HelperJson.DeserializeObject<Entity.Respose.GTXResult>(json);
            string resultjson = "[{'AOMid':'1009','NormalScore':'90','ExamScore':'0'},{'AOMid':'1092','NormalScore':'80','ExamScore':'90'},{'AOMid':'1013','NormalScore':'90','ExamScore':'80'},{'AOMid':'1135','NormalScore':'70','ExamScore':'80'}]";
            if (_OLSchoolUserId.EndsWith("2"))
            {
                resultjson = "[{'AOMid':'1009','NormalScore':'95','ExamScore':'0'},{'AOMid':'1092','NormalScore':'80','ExamScore':'95'},{'AOMid':'1013','NormalScore':'90','ExamScore':'80'},{'AOMid':'1135','NormalScore':'70','ExamScore':'85'}]";
            }
            List<Entity.Respose.scoredetail> scdetaillist = Untity.HelperJson.DeserializeObject<List<Entity.Respose.scoredetail>>(Untity.HelperDataCvt.objToString(resultjson));
            if (scdetaillist == null || scdetaillist.Count == 0)
            {
                return new List<Entity.Respose.scoredetail>();
            }
            else
            {
                return scdetaillist;
            }
        }

        public static long GetTicketCount(string studentid, string certificateid)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_StudentTicket WHERE IsDel = 0 AND dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertificateId='{1}' ", studentid, certificateid);
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

        public static long Add(Entity.MsSQL.T_StudentTicket model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_StudentTicket(");
            strSql.Append("CertificateId,OrgaizId,StudentId,TicketNum,OLMobile)");
            strSql.Append(" values (");
            strSql.Append("@CertificateId,@OrgaizId,@StudentId,@TicketNum,@OLMobile)");
            SqlParameter[] parameters = {
                    new SqlParameter("@CertificateId", SqlDbType.VarChar,100),
                    new SqlParameter("@OrgaizId", SqlDbType.BigInt,8),
                    new SqlParameter("@StudentId", SqlDbType.VarChar,100),
                    new SqlParameter("@TicketNum", SqlDbType.VarChar,100),
                    new SqlParameter("@OLMobile", SqlDbType.VarChar,20),
                                        };
            parameters[0].Value = model.CertificateId;
            parameters[1].Value = model.OrgaizId;
            parameters[2].Value = model.StudentId;
            parameters[3].Value = model.TicketNum;
            parameters[4].Value = model.OLMobile;


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

        public static string GetOLMobile(string _OLSchoolUserId)
        {
            Untity.HelperMethod p = new Untity.HelperMethod();
            string path = Untity.HelperAppSet.getAppSetting("olschoolpath_mb");
            string fullpath = path + "/api?userid=" + _OLSchoolUserId;
            string json = p.Get(fullpath);
            Entity.Respose.getolmobile result = Untity.HelperJson.DeserializeObject<Entity.Respose.getolmobile>(json);
            if (result != null && !string.IsNullOrEmpty(result.mobile))
            {
                return result.mobile;
            }
            else
            {
                return "";
            }
        }

        public static Entity.Respose.getcertifiprint GetCertifiPrintModel(string _serialnum)
        {
            string sql = string.Format("select T_Student.HeaderUrl,T_Student.Name,T_Student.CardId,T_Student.OLSchoolUserId,(case when T_Student.Sex=1 then '男' else '女' end) as Sex, " +
                            "T_Certificate.CategoryName,T_Certificate.ExamSubject, " +
                            "T_CertifiSerial.SerialNum,T_CertifiSerial.IssueDate,T_StudentTicket.Id from T_StudentTicket " +
                            "left join T_Certificate on T_StudentTicket.CertificateId=T_Certificate.Id " +
                            "left join T_Student on T_StudentTicket.StudentId=T_Student.Id " +
                            "left join T_CertifiSerial on T_StudentTicket.SerialNum=T_CertifiSerial.SerialNum " +
                            "where T_StudentTicket.SerialNum='{0}'", _serialnum);
            List<Entity.Respose.getcertifiprint> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getcertifiprint>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }
        //打印准考证
        public static Entity.Respose.getticketprint GetTicketPrintModel(string _ticketnum)
        {
            string sql = string.Format("select convert(varchar(4),T_Certificate.StartTime,120) as StartTime, " +
                            "T_Student.Name,(case when T_Student.Sex=1 then '男' else '女' end) as Sex,T_Student.CardId, " +
                            "T_Student.HeaderUrl,T_StudentTicket.TicketNum from T_StudentTicket left join T_Certificate " +
                            "on T_StudentTicket.CertificateId=T_Certificate.Id " +
                            "left join T_Student on T_StudentTicket.StudentId=T_Student.Id " +
                            "where T_StudentTicket.TicketNum='{0}'", _ticketnum);
            List<Entity.Respose.getticketprint> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getticketprint>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }
        //打印准考证考场科目信息
        public static Entity.Respose.getticketprintInfo GetTicketPrintInfoModel(string _ticketnum)
        {
            string sql = string.Format("SELECT ExamName , ExamPlace,CentreName,ExamNum,ResultReleaseTime,SeatNumber,CategoryName,ExamSubject,StartTime,EndTime" +
                                        " FROM T_ExamRoom, T_ExamSeat, T_StudentTicket, T_Certificate" + 
                                        " WHERE T_StudentTicket.TicketNum = T_ExamSeat.TicketId" +
                                        " AND T_StudentTicket.CertificateId = T_Certificate.Id AND T_ExamSeat.ExamRoomId = T_ExamRoom.id" +
                                        " AND T_StudentTicket.TicketNum = '{0}'", _ticketnum);
            List<Entity.Respose.getticketprintInfo> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.Respose.getticketprintInfo>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public static void Delete(string ticketnum)
        {
            string sql = string.Format("UPDATE dbo.T_StudentTicket SET IsDel = 1 WHERE TicketNum = '{0}' ", ticketnum);
            Untity.HelperMsSQL.ExecuteQuery(sql);
        }

        public static dynamic getStudentsByCertificateID(string postString)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string certificateId = JObject.Parse(postString)["certificateId"].ToString();
            dynamic re_obj = new JArray();
            if (!string.IsNullOrEmpty(certificateId))
            {
                var getByWhere = db.Queryable<T_StudentTicket, T_Student, T_Organiza>((a, b, c) => new object[] { JoinType.Inner, a.StudentId == b.Id, JoinType.Inner, b.OrgaId == c.Id }).Where((a, b) => a.CertificateId == certificateId && a.IsDel != MySetting.IsDel).Select((a, b, c) => new { studentId = b.Id, studentName = b.Name, StudentTicketId = a.Id, b.OLSchoolUserId, b.OLSchoolUserName, c.ClassId, index = SqlFunc.MappingColumn(b.Id, "row_number() over(order by b.id)") }).ToList();

                re_obj = getByWhere;
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            return re_obj;
        }

        /// <summary>
        /// 更改状态为已考试
        /// </summary>
        public static void updatestateto2(string examid)
        {
            var obj = db.Updateable<Entity.MsSQL.T_StudentTicket>().UpdateColumns(it => new { State = Entity.Enum.TicketState.已考试 }).Where(it => it.TicketNum == examid).ExecuteCommand();
        }
        public static int GetStudentCountByCertificateId(string certificateids)
        {
            string sql = string.Format("SELECT count(*) FROM dbo.T_StudentTicket WHERE IsDel = 0 AND CertificateId ='{0}' ", certificateids);
            // string sql = string.Format("SELECT count(*) FROM dbo.T_StudentTicket WHERE IsDel = 0 AND dbo.T_StudentTicket.StudentId = '{0}' AND dbo.T_StudentTicket.CertificateId='{1}' ", studentid, certificateid);
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
