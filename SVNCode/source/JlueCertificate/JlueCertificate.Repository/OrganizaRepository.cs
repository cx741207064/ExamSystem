using JlueCertificate.Dal.MsSQL;
using JlueCertificate.Tool;
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
            ConnectionConfig connectionConfig = new ConnectionConfig()
            {
                ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ToString().Trim(),
                DbType = DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            };
            _db = new SqlSugarClient(connectionConfig);
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

        private static SqlSugarClient _db;

        private static SqlSugarClient db
        {
            get
            {
                return _db;
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
            var state = Dal.MsSQL.T_StudentTicket.GetTicketState(studentId, certificateId);

            if (state == "" || state == null)
            {
                db.Updateable<T_StudentTicket>().SetColumns(it => new T_StudentTicket() { SerialNum = SerialNum }).Where(it => it.CertificateId == certificateId && it.StudentId == studentId).ExecuteCommand();
                long BigIdentity = db.Insertable(new T_CertifiSerial() { CertificateId = certificateId, State = "1", SerialNum = SerialNum, IssueDate = DateTime.Now, CreateTime = DateTime.Now, IsDel = "0" }).ExecuteReturnBigIdentity();
                result.Msg = "颁发成功";
            }
            else
            {
                result.Msg = "证书已颁发";
            }

            return Untity.HelperJson.SerializeObject(result);

        }

        public Entity.MsSQL.T_Student GetStudentModel(string studentid)
        {
            List<Entity.MsSQL.T_Student> list = db.Queryable<Entity.MsSQL.T_Student>().Where(t => t.Id == studentid).ToList();
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }

        public HttpResponseResult OpenLearningSystemCertificate(string UserName, string UserPass, Entity.MsSQL.T_Certificate certificate)
        {
            HttpResponseResult hrr = new HttpResponseResult();
            Dictionary<string, int> certificateNo = GetCertificateNo();
            int Star = certificateNo.Where(a => (certificate.CategoryName + certificate.ExamSubject).IndexOf(a.Key) > 0).FirstOrDefault().Value;
            if (Star == 0)
            {
                hrr.Message = "证书格式不符合规范";
                return hrr;
            }
            byte[] b = Encoding.UTF8.GetBytes("chun815@tom.com".Substring(0, 8));
            string str = string.Format("UserName={0}&UserPass={1}&Star={2}", UserName, UserPass, Star);
            string en = PublicMethod.DesEncrypt(str, b, b);
            string requestUri = string.Format("http://cwrcpjhoutai.kjcytk.com/api/InterFace/AddUserCourse?sign={0}", en);
            var result = HttpHelper.Singleton.HttpPost(requestUri);
            hrr = result.Result;
            return hrr;
        }

        private Dictionary<string, int> GetCertificateNo()
        {
            Dictionary<string, int> certificateNo = new Dictionary<string, int>() { { "一星A", 1 }, { "一星B", 2 }, { "一星C", 3 }, { "二星A", 4 }, { "二星B1", 5 }, { "二星B2", 6 }, { "二星C1", 7 }, { "二星C2", 8 }, { "三星A", 9 }, { "三星B", 10 }, { "三星C", 11 } };
            return certificateNo;
        }

    }
}
