using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    

    [Serializable]
    public class getunsignupcertificate
    {
        public getunsignupcertificate()
        {
            unsignup = new List<unsignupcertificate>();
            name = "";
            cardid = "";
            username = "";
        }
        public string name { get; set; }
        public string cardid { get; set; }
        public string username { get; set; }
        public List<unsignupcertificate> unsignup { get; set; }
    }

    [Serializable]
    public class getsignupcertificate
    {
        public getsignupcertificate()
        {
            signup = new List<signupcertificate>();
            name = "";
            cardid = "";
        }
        public string name { get; set; }
        public string cardid { get; set; }
        public List<signupcertificate> signup { get; set; }
    }

    [Serializable]
    public class getholdcertificate
    {
        public getholdcertificate()
        {
            hold = new List<holdcertificate>();
            name = "";
            cardid = "";
        }
        public string name { get; set; }
        public string cardid { get; set; }
        public List<holdcertificate> hold { get; set; }
    }

    [Serializable]
    public class unsignupcertificate
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string Describe { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SubjectIds { get; set; }
        public List<allcertifisubject> certifisubjects { get; set; }
    }

    [Serializable]
    public class signupcertificate
    {
        public string Id { get; set; }
        public string TicketNum { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    [Serializable]
    public class holdcertificate
    {
        public string Id { get; set; }
        public string SerialNum { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string CreateTime { get; set; }
        public string CertState { get; set; }
    }
}
