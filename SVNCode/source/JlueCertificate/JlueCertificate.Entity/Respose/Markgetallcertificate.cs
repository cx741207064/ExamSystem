using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class Markgetallcertificate
    {
        public string Id { set; get; }
        public string CategoryName { set; get; }
        public string ExamSubject { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string NormalResult { set; get; }
        public string ExamResult { set; get; }
        public string Rule { set; get; }
        public string Describe { set; get; }
        public string IsDel { set; get; }
        public string CreateTime { set; get; }
    }
}
