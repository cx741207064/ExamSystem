using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class UserInfo
    {
        public UserInfo()
        {
            subjects = new List<SubjectInfo>();
        }
        public string certifiid { get; set; }
        public string studentname { get; set; }
        public List<SubjectInfo> subjects { get; set; }
    }

    [Serializable]
    public class SubjectInfo
    {
        public string subjectid { get; set; }
        public string name { get; set; }
    }

    [Serializable]
    public class OLUserResponse
    {
        public string id { get; set; }
        public string msg { get; set; }
    }
}
