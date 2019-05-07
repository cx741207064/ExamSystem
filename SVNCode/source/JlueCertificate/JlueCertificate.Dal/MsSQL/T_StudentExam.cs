using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_StudentExam
    {
        public static Entity.MsSQL.T_StudentExam GetModel(string id)
        {
            string sql = string.Format("SELECT [ExamId],[StudentId],[SchemeId],[OrganizaId],[PayType],[IsDel],[CreateTime] FROM [dbo].[T_StudentExam] WHERE ExamId = {0}", id);
            List<Entity.MsSQL.T_StudentExam> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_StudentExam>(sql);
            if (list == null || list.Count == 0)
            {
                return null;
            }
            else
            {
                return list.FirstOrDefault();
            }
        }
    }
}
