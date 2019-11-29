using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
   public class getexamroom
    {
        public int id { get; set; }
        public string ExamName { get; set; }
        public string ExamPlace { get; set; }
        public string CentreName { get; set; }
        public string ExamNum { get; set; }
        public string SeatNum { get; set; }
        
        public string createtime { get; set; }
        public string IsDel { get; set; }
    }
}
