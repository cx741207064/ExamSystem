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

        public int MarkUserId { get; set; }

        public string CertificateId { get; set; }

        private static SqlSugarClient db
        {
            get
            {
                ConnectionConfig connectionConfig = new ConnectionConfig()
                {
                    ConnectionString = Untity.HelperMsSQL.connStr,
                    DbType = DbType.SqlServer,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                };
                return new SqlSugarClient(connectionConfig);
            }
        }

        public static void Certificate(Entity.Request.addmarkuser model)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            db.Deleteable<T_MarkUserCertificate>().Where(it => it.MarkUserId == int.Parse(model.id)).ExecuteCommand();

            var insertObjs = new List<T_MarkUserCertificate>();
            foreach (JToken item in model.certificate)
            {
                T_MarkUserCertificate insertObj = new T_MarkUserCertificate { MarkUserId = int.Parse(model.id), CertificateId = item.ToString() };
                insertObjs.Add(insertObj);
            }
            if (insertObjs.Count > 0)
            {
                db.Insertable(insertObjs.ToArray()).IgnoreColumns(it => it.Id).ExecuteCommand();
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
        }

        public static dynamic getMarkUserCertificateById(string MarkUserId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            dynamic re_obj;
            if (string.IsNullOrEmpty(MarkUserId))
            {
                var getByWhere = db.Queryable<T_Certificate>().Where(a => a.IsDel == "0").Select(a => new { id = a.Id, categoryName = a.CategoryName, examSubject = a.ExamSubject, isChecked = false, index = SqlFunc.MappingColumn(a.Id, "row_number() over(order by id)") }).ToList();
                re_obj = getByWhere;
            }
            else
            {
                var getByWhere = db.Queryable<T_Certificate, T_MarkUserCertificate>((a, b) => new object[] { JoinType.Left, a.Id == b.CertificateId && b.MarkUserId.ToString() == MarkUserId }).Where(a => a.IsDel == "0").Select((a, b) => new { id = a.Id, categoryName = a.CategoryName, examSubject = a.ExamSubject, isChecked = string.IsNullOrEmpty(b.MarkUserId.ToString()) ? false : true, index = SqlFunc.MappingColumn(b.Id, "row_number() over(order by a.id)") }).ToList();

                re_obj = getByWhere;
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
            return re_obj;
        }

        public static dynamic getMarkUserCertificateByName(string MarkUserName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string name = JObject.Parse(MarkUserName)["name"].ToString();
            dynamic re_obj = new object();
            if (!string.IsNullOrEmpty(name))
            {
                var getByWhere = db.Queryable<T_MarkUser, T_MarkUserCertificate, T_Certificate>((a, b, c) => new object[] { JoinType.Inner, a.Id == b.MarkUserId && a.Name == name, JoinType.Inner, b.CertificateId == c.Id }).Where((a, b, c) => c.IsDel == "0").Select((a, b, c) => new { id = c.Id, categoryName = c.CategoryName, examSubject = c.ExamSubject, index = SqlFunc.MappingColumn(c.Id, "row_number() over(order by a.id)") }).ToList();

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

            var getByWhere = db.Queryable<T_MarkUserCertificate>().Where(it => it.MarkUserId == int.Parse(model.id)).ToList();
            if (getByWhere.Count > 0)
            {
                db.Deleteable<T_MarkUserCertificate>().Where(getByWhere).ExecuteCommand();
            }
            watch.Stop();
            TimeSpan ts = watch.Elapsed;
        }

    }
}
