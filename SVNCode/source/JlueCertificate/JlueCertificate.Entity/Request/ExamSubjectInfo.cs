using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class ExamSubjectInfo
    {
        public string certifiid { get; set; }
        public string subjectid { get; set; }
    }
}
