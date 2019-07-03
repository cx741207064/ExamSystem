using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_StudentSubjectScore
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        public string studentid { get; set; }

        public string aomid { get; set; }

        public string score { get; set; }

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

        public static object getStudentSubjectScore(string studentid, string aomid)
        {
            var query = db.Queryable<T_StudentSubjectScore>().Where(a => a.studentid == studentid && a.aomid == aomid);
            return query;
        }

        public static object addStudentSubjectScore(T_StudentSubjectScore sss)
        {
            var obj = db.Insertable<T_StudentSubjectScore>(sss).IgnoreColumns(it => it.id).ExecuteCommand();
            return obj;
        }

        public static object updateStudentSubjectScore(T_StudentSubjectScore sss)
        {
            var obj = db.Updateable(sss).ExecuteCommand();
            return obj;
        }
    }
}
