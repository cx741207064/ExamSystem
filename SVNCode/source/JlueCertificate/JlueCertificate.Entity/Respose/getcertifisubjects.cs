using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class allcertifisubject
    {
        public string CertificateId { get; set; }
        public string SubjectId { get; set; }
        public string OLSchoolId { get; set; }
        public string OLSchoolAOMid { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string NormalResult { get; set; }
        public string ExamResult { get; set; }
    }
}
