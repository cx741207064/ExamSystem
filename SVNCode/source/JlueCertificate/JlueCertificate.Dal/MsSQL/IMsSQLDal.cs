using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Dal.MsSQL
{
    interface IMsSQLDal
    {
        long insert();
        long update();
        long select();
        long save();
    }
}
