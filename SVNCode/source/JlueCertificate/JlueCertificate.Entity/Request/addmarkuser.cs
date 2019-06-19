using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class addmarkuser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string pwd { get; set; }
        public string level { get; set; }

        public JArray certificate { get; set; }

    }
}
