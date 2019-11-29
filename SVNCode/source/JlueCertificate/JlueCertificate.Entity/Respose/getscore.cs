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
        public string SerialNum { get; set; }
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

    public class HKJScore
    {
        public string ScoreId { get; set; }
        public string CourseId { get; set; }
        public string Identify { get; set; }
        public string BookId { get; set; }
        public string IsExam { get; set; }
        public decimal AutoTotal { get; set; }
        public decimal AutoRight { get; set; }
        public decimal AutoFinal { get; set; }
        public decimal ManualTotal { get; set; }
        public decimal ManualRight { get; set; }
        public decimal ManualFinal { get; set; }
        public int IsDel { get; set; }
        public DateTime CreatedTime { get; set; }
    }

    public class SPScore
    {
        public decimal persent { get; set; }
        public int count { get; set; }
        public int sum { get; set; }
    }

    public class normalscore
    {
        public string subjectId { get; set; }
        public string subjectName { get; set; }
        public string subjectType { get; set; }
        public decimal score { get; set; }
        public string Sort_Id { get; set; }
        public string AOMid { get; set; }
        public string classId { get; set; }
        public int index { get; set; }
    }
}
