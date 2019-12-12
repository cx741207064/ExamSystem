using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Tool
{
    public class HttpResponseResult
    {
        public bool isSuccess { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public string Data { get; set; }

    }
}
