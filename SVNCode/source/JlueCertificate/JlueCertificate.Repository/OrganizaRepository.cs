using JlueCertificate.Dal.MsSQL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Repository
{
    public sealed class OrganizaRepository
    {
        private OrganizaRepository()
        {
        }

        private static object locker = new object();

        private static OrganizaRepository _instance = null;

        public static OrganizaRepository Singleton
        {
            get
            {
                lock (locker)
                {
                    if (_instance == null)
                    {
                        _instance = new OrganizaRepository();
                    }
                    return _instance;
                }
            }
        }

        private static SqlSugarClient db
        {
            get
            {
                ConnectionConfig connectionConfig = new ConnectionConfig()
                {
                    ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ToString().Trim(),
                    DbType = DbType.SqlServer,//设置数据库类型
                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                };
                return new SqlSugarClient(connectionConfig);
            }
        }

        public void UpdateIDCardPath(string stuid, string side, string path)
        {
            var t = db.Queryable<T_Student>().Where(it => it.Id == stuid).First();
            if (t == null)
            {
                return;
            }
            JObject j;
            if (string.IsNullOrEmpty(t.UploadIDCardPath))
            {
                j = new JObject();
            }
            else
            {
                j = JObject.Parse(t.UploadIDCardPath);
            }
            j[side] = path;
            var data = new T_Student() { Id = stuid, UploadIDCardPath = JsonConvert.SerializeObject(j) };
            db.Updateable(data).UpdateColumns(it => new { it.UploadIDCardPath }).ExecuteCommand();
        }

        public void UpdateHeader(string stuid, string path)
        {
            var data = new T_Student() { Id = stuid, HeaderUrl = path };
            db.Updateable(data).UpdateColumns(it => new { it.HeaderUrl }).ExecuteCommand();
        }

        public string IssueCertificate(string studentId, string certificateId)
        {
            Untity.HelperHandleResult result = new Untity.HelperHandleResult();

            string SerialNum = "ZS" + DateTime.Now.Ticks;
            db.Updateable<T_StudentTicket>().SetColumns(it => new T_StudentTicket() { SerialNum = SerialNum }).Where(it => it.CertificateId == certificateId && it.StudentId == studentId).ExecuteCommand();
            long BigIdentity = db.Insertable(new T_CertifiSerial() { CertificateId = certificateId, State = "1", SerialNum = SerialNum, IssueDate = DateTime.Now, CreateTime = DateTime.Now, IsDel = "0" }).ExecuteReturnBigIdentity();
            return Untity.HelperJson.SerializeObject(result);

        }

    }
}
