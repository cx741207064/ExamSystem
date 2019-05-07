using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class getscore
    {
        public getscore()
        {
            all = new List<ticket>();
        }
        public List<ticket> all { get; set; }
    }

    [Serializable]
    public class getscoredetail
    {
        public getscoredetail()
        {
            all = new List<scoredetail>();
            scoresum = "";
            examresult = "";
            accountform = "";
            resultstd = "";
        }
        public string scoresum { get; set; }
        public string examresult { get; set; }
        public string accountform { get; set; }
        public string resultstd { get; set; }
        public List<scoredetail> all { get; set; }
    }

    [Serializable]
    public class ticket
    {
        public long Id { get; set; }
        public string TicketNum { get; set; }
        public string CategoryName { get; set; }
        public string ExamSubject { get; set; }
        public string Name { get; set; }
        public string OLSchoolUserId { get; set; }
        public string OLSchoolUserName { get; set; }
        public string CreateTime { get; set; }
    }


    [Serializable]
    public class scoredetail
    {
        public string AOMid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string NormalScore { get; set; }
        public string ExamScore { get; set; }
        public string NormalResult { get; set; }
        public string ExamResult { get; set; }
    }
}
