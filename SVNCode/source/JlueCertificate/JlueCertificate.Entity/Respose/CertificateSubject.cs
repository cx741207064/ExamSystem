using JlueCertificate.Entity.MsSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    public class GetCertificateSubject
    {
        public string index { get; set; }

        public string id { get; set; }

        public string categoryName { get; set; }

        public string examSubject { get; set; }

        public List<T_Subject> subjects { get; set; }

    }
}
