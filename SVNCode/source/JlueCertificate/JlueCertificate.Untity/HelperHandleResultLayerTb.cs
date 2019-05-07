using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlueCertificate.Untity
{
    [Serializable]
    public class HelperHandleResultLayerTb
    {
        public HelperHandleResultLayerTb()
        {
            code = "0";
            msg = "";
            count = "0";
        }
        public string code { get; set; }
        public string msg { get; set; }
        public string count { get; set; }
        public object data { get; set; }
    }
}
