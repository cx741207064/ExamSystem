using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class getstudentcertifi
    {
        public getstudentcertifi()
        {
            unsignup = new List<studentcertifi>();
            signup = new List<studentcertifi>();
            hold = new List<studentcertifi>();
            name = "";
            cardid = "";
            username = "";
        }
        public string name { get; set; }
        public string cardid { get; set; }
        public string username { get; set; }
        public List<studentcertifi> unsignup { get; set; }
        public List<studentcertifi> signup { get; set; }
        public List<studentcertifi> hold { get; set; }
    }

    [Serializable]
    public class studentcertifi
    {
        public studentcertifi()
        {
            Subject = new List<allcertifisubject>();
        }
        public string CertifiId { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string Describe { get; set; }
        public string NormalResult { get; set; }
        public string ExamResult { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string IssueDate { get; set; }
        public string TicketNum { get; set; }
        public string SerialNum { get; set; }
        public string CertState { get; set; }
        public List<allcertifisubject> Subject { get; set; }
    }
}
