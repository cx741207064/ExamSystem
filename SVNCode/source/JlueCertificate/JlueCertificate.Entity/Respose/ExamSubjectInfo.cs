using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class ExamSubjectInfo
    {
        public string certifiid { get; set; }
        public string subjectid { get; set; }
        public string subjectname { get; set; }
        public bool iswinopen { get; set; }
        public string url { get; set; }
    }
}
