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
            subjects = new List<CertificateSubjectInfo>();
        }
        public string certificateId { get; set; }

        public string certificateName { get; set; }

        public string certificateLevel { get; set; }

        public string certificateStartTime { get; set; }

        public string certificateEndTime { get; set; }

        public string studentId { get; set; }

        public string studentName { get; set; }

        public List<CertificateSubjectInfo> subjects { get; set; }
    }

    [Serializable]
    public class CertificateSubjectInfo
    {
        public long index { get; set; }

        public long subjectId { get; set; }

        public string subjectName { get; set; }
    }

}