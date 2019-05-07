using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class handelorganiza
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string AppName { get; set; }
        public string ClassId { get; set; }
        public string Path { get; set; }
        public string Describe { get; set; }
    }
}
