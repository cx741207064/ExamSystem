using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class handelcertifisubject
    {
        public string ID { get; set; }
        public string ExamResult { get; set; }
        public string NormalResult { get; set; }
        public string SubjectId { get; set; }
        public string CertificateId { get; set; }
        public string IsNeedExam { get; set; }
        public string ExamLength { get; set; }
        public string CertId { get; set; }
    }
}
