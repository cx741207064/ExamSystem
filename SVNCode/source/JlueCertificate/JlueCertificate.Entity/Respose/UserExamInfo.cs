using JlueCertificate.Entity.MsSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class UserExamInfo
    {
        public UserExamInfo()
        {
            subjects = new List<T_Subject>();
        }
        public int StudentTicketId { get; set; }

        public string certificateId { get; set; }

        public string certificateName { get; set; }

        public string certificateLevel { get; set; }

        public string certificateStartTime { get; set; }

        public string certificateEndTime { get; set; }

        public string studentId { get; set; }

        public string studentName { get; set; }

        public string CardId { get; set; }

        public string orgPath { get; set; }

        public string orgClassId { get; set; }

        public string OLSchoolUserId { get; set; }

        public string OLSchoolUserName { get; set; }

        public string OLSchoolPWD { get; set; }

        public List<T_Subject> subjects { get; set; }
    }

    [Serializable]
    public class CertificateSubjectInfo
    {
        public long index { get; set; }

        public long subjectId { get; set; }

        public string subjectName { get; set; }
    }

}