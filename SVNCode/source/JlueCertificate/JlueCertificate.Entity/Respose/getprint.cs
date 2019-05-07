using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    public class getcertifiprint
    {
        public string Id { get; set; }
        public string HeaderUrl { get; set; }
        public string Name { get; set; }
        public string CardId { get; set; }
        public string Sex { get; set; }
        public string OLSchoolUserId { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string SerialNum { get; set; }
        public string IssueDate { get; set; }
    }

    public class getticketprint
    {
        public string StartTime { get; set; }
        public string TicketNum { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string CardId { get; set; }
        public string HeaderUrl { get; set; }
    }
}
