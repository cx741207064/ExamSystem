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
    public class getticketprintInfo
    {
        public string ExamName { get; set; }
        public string ExamPlace { get; set; }
        public string CentreName { get; set; }
        public string ExamNum { get; set; }
        public DateTime ResultReleaseTime { get; set; }
        public string SeatNumber { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
    public class getexamInfo
    {
        public int id { get; set; }
        public string ExamName { get; set; }
        public string ExamPlace { get; set; }
        public string CentreName { get; set; }
        public string ExamNum { get; set; }
    }
    public class getexamseatInfo
    {
        public int id { get; set; }
        public string ExamRoomId { get; set; }
        public string SeatNumber { get; set; }
        public string TicketId { get; set; }
    }
}
