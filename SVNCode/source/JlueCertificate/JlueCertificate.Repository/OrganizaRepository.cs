using JlueCertificate.Dal.MsSQL;
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

        public void UpdateIDCardPath(string stuid, string path)
        {
            var data = new T_Student() { Id = stuid, UploadIDCardPath = path };
            db.Updateable(data).UpdateColumns(it => new { it.UploadIDCardPath }).ExecuteCommand();
        }

        public void UpdateHeader(string stuid, string path)
        {
            var data = new T_Student() { Id = stuid, HeaderUrl = path };
            db.Updateable(data).UpdateColumns(it => new { it.HeaderUrl }).ExecuteCommand();
        }

    }
}
