using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_MarkUserCertificate
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }

        public string MarkUserId { get; set; }

        public string CertificateId { get; set; }

        private static ConnectionConfig connectionConfig
        {
            get
            {
                return new ConnectionConfig()
                {
                    ConnectionString = Untity.HelperMsSQL.connStr,
                    DbType = DbType.SqlServer,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                };
            }
        }

        public static void Certificate(Entity.Request.addmarkuser model)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlSugarClient db = new SqlSugarClient(connectionConfig);
            var getByWhere = db.Queryable<T_MarkUserCertificate>().Where(it => it.MarkUserId == model.id).ToList();
            if (getByWhere.Count > 0)
            {
                db.Deleteable<T_MarkUserCertificate>().Where(getByWhere).ExecuteCommand();
            }

            var insertObjs = new List<T_MarkUserCertificate>();
            foreach (JToken item in model.certificate)
            {
                T_MarkUserCertificate insertObj = new T_MarkUserCertificate { MarkUserId = model.id, CertificateId = item.ToString() };
                insertObjs.Add(insertObj);
            }
            if (insertObjs.Count > 0)
            {
                db.Insertable(insertObjs.ToArray()).IgnoreColumns(it => it.Id).ExecuteCommand();
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
        }

        public static dynamic getCertificate(string MarkUserId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            dynamic re_obj;
            SqlSugarClient db = new SqlSugarClient(connectionConfig);
            if (string.IsNullOrEmpty(MarkUserId))
            {
                var getByWhere = db.Queryable<T_Certificate>().Where(a => a.IsDel == "0").Select(a => new { id = a.Id, categoryName = a.CategoryName, examSubject = a.ExamSubject, isChecked = false, index = SqlFunc.MappingColumn(a.Id, "row_number() over(order by id)") }).ToList();
                re_obj = getByWhere;
            }
            else
            {
                var getByWhere = db.Queryable<T_Certificate, T_MarkUserCertificate>((a, b) => new object[] { JoinType.Left, a.Id == b.CertificateId && b.MarkUserId == MarkUserId }).Where(a => a.IsDel == "0").Select((a, b) => new { id = a.Id, categoryName = a.CategoryName, examSubject = a.ExamSubject, isChecked = string.IsNullOrEmpty(b.MarkUserId) ? false : true, index = SqlFunc.MappingColumn(b.Id, "row_number() over(order by a.id)") }).ToList();

                re_obj = getByWhere;
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            return re_obj;
        }

        public static void deleteMarkUserCertificate(Entity.Respose.getmarkuser model)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlSugarClient db = new SqlSugarClient(connectionConfig);
            var getByWhere = db.Queryable<T_MarkUserCertificate>().Where(it => it.MarkUserId == model.id).ToList();
            if (getByWhere.Count > 0)
            {
                db.Deleteable<T_MarkUserCertificate>().Where(getByWhere).ExecuteCommand();
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
        }

    }
}
