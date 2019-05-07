using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class addstudent
    {
        public string idnumber {get;set;}
        public string orgaid {get;set;}
        public string name {get;set;}
        public string cardid { get; set; }
        public string headerurl { get; set; }
        public string sex { get; set; }
        public string telphone { get; set; }
        public string provinceid { get; set; }
        public string cityid { get; set; }
        public string zoneid { get; set; }
        public string address { get; set; }
        public string postprovinceid { get; set; }
        public string postcityid { get; set; }
        public string postzoneid { get; set; }
        public string postaddress { get; set; }
    }
}
