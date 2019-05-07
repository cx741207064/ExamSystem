using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    public class T_ProvinceCity
    {

        public static List<Entity.MsSQL.T_ProvinceCity> GetList()
        {
            string sql = "SELECT * FROM dbo.T_ProvinceCity ";
            List<Entity.MsSQL.T_ProvinceCity> list = Untity.HelperMsSQL.ExecuteQueryToList<Entity.MsSQL.T_ProvinceCity>(sql);
            if (list == null || list.Count == 0)
            {
                return new List<Entity.MsSQL.T_ProvinceCity>();
            }
            else
            {
                return list;
            }
        }
    }
}
