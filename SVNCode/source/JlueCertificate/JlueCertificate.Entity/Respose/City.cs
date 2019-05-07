using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Respose
{
    [Serializable]
    public class Province
    {
        public Province()
        {
            citys = new List<City>();
        }
        public string id { get; set; }
        public string name { get; set; }
        public List<City> citys { get; set; }
    }


    [Serializable]
    public class City
    {
        public City()
        {
            zones = new List<Zone>();
        }
        public string id { get; set; }
        public string name { get; set; }
        public List<Zone> zones { get; set; }
    }


    [Serializable]
    public class Zone
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
