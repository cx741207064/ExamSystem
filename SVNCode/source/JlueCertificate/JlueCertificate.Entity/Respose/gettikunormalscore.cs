using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    public class Sorts
    {
        public Sorts()
        {
            papers = new List<Papers>();
        }
        public string name { get; set; }
        public List<Papers> papers { get; set; }
    }


    public class Papers
    {
        public Papers()
        {
            scores = new List<PaperScores>();
        }
        public List<PaperScores> scores { set; get; }
        public string papername { get; set; }
        public string paperid { get; set; }
    }

    public class PaperScores
    {
        public decimal Score { set; get; }
        public string CreateDate { set; get; }
    }
}
