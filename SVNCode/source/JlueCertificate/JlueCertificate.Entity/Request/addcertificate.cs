using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class addcertificate
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Describe { get; set; }
        public string NormalResult { get; set; }
        public string ExamResult { get; set; }
        public string SubjectIds { get; set; }
    }
}
