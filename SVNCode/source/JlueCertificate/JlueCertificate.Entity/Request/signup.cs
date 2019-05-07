using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class signup
    {
        public string name { get; set; }
        public string cardid { get; set; }
        public string studentid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public long certificateid { get; set; }
    }
}
