using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class handelsubject
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Describe { get; set; }
        public string OLSchoolId { get; set; }
        public string OLSchoolName { get; set; }
        public string OLSchoolProvinceId { get; set; }
        public string OLSchoolCourseId { get; set; }
        public string OLSchoolQuestionNum { get; set; }
        public string OLSchoolAOMid { get; set; }
        public string OLSchoolMasterTypeId { get; set; }
    }
}
