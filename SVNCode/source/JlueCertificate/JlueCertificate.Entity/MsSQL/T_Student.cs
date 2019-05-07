using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public class T_Student
    {
        public T_Student()
        { }
        #region Model
        private string _id;
        private long _orgaid;
        private string _name;
        private string _cardid;
        private string _headerurl;
        private string _sex;
        private string _telphone;
        private string _provinceid;
        private string _cityid;
        private string _zoneid;
        private string _address;
        private string _postprovinceid;
        private string _postcityid;
        private string _postzoneid;
        private string _postaddress;
        private string _isdel = "0";
        private DateTime _createtime = DateTime.Now;
        private string _olschooluserid;
        private string _olschoolusername;
        private string _olschoolpwd;
        /// <summary>
        /// 
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long OrgaId
        {
            set { _orgaid = value; }
            get { return _orgaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardId
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HeaderUrl
        {
            set { _headerurl = value; }
            get { return _headerurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProvinceId
        {
            set { _provinceid = value; }
            get { return _provinceid; }
        }
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
        public string ZoneId
        {
            set { _zoneid = value; }
            get { return _zoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostProvinceId
        {
            set { _postprovinceid = value; }
            get { return _postprovinceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostCityId
        {
            set { _postcityid = value; }
            get { return _postcityid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostZoneId
        {
            set { _postzoneid = value; }
            get { return _postzoneid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PostAddress
        {
            set { _postaddress = value; }
            get { return _postaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolUserId
        {
            set { _olschooluserid = value; }
            get { return _olschooluserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolUserName
        {
            set { _olschoolusername = value; }
            get { return _olschoolusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolPWD
        {
            set { _olschoolpwd = value; }
            get { return _olschoolpwd; }
        }
        #endregion Model

    }
}
