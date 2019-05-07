using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_ProvinceCity
    {
        public T_ProvinceCity()
        { }
        #region Model
        private string _cityid;
        private string _cityname;
        private string _areacode;
        private string _postcode;
        private string _coordinates;
        private string _level;
        private string _levelcode;
        /// <summary>
        /// 
        /// </summary>
        public string CityId
        {
            set { _cityid = value; }
            get { return _cityid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Coordinates
        {
            set { _coordinates = value; }
            get { return _coordinates; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Level
        {
            set { _level = value; }
            get { return _level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LevelCode
        {
            set { _levelcode = value; }
            get { return _levelcode; }
        }
        #endregion Model
    }
}
