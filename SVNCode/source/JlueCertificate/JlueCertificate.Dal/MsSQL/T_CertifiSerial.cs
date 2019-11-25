using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_CertifiSerial
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        public string CertificateId { get; set; }

        public string SerialNum { get; set; }

        public string State { get; set; }

        public string IsDel { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime IssueDate { get; set; }

        public static List<Entity.MsSQL.T_CertifiSerial> GetList(string studentid)
        {
            string sql = string.Format("SELECT * FROM dbo.T_CertifiSerial WHERE StudentId = '{0}' AND IsDel = 0 ", studentid);
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_CertifiSerial>(sql);
        }

        public static List<Entity.MsSQL.T_CertifiSerial> GetAllList()
        {
            string sql = string.Format("SELECT * FROM dbo.T_CertifiSerial WHERE IsDel = 0 ");
            return Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_CertifiSerial>(sql);
        }
    }
}
