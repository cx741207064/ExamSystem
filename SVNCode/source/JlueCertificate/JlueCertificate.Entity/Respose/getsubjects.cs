using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class getsubjectsbycertid
    {
        public getsubjectsbycertid()
        {
            all = new List<subjectsbycertid>();
        }
        public List<subjectsbycertid> all { get; set; }
    }

    [Serializable]
    public class getolschoolsubjects
    {
        public getolschoolsubjects()
        {
            all = new List<olschoolsubject>();
        }
        public List<olschoolsubject> all { get; set; }
    }

    [Serializable]
    public class olschoolsubject
    {
        public string Sort_Id { get; set; }
        public string Sort_Name { get; set; }
        public string ProvinceId { get; set; }
        public string CourseId { get; set; }
        public string QuestionNum { get; set; }
        public string AOMid { get; set; }
        public string MasterTypeId { get; set; }
    }

    [Serializable]
    public class subjectsbycertid
    {
        public string ID { get; set; }
        public string SubjectId { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string NormalResult { get; set; }
        public string ExamResult { get; set; }
        public string IsNeedExam { get; set; }
        public string ExamLength { get; set; }
        public bool LAY_CHECKED { get; set; }
    }
}
