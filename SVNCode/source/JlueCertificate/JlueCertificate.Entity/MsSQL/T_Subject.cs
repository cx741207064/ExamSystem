using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_Subject
    {
        public T_Subject()
        { }
        #region Model
        private long _id;
        private string _name;
        private string _category;
        private string _price;
        private string _describe;
        private string _olschoolid;
        private string _olschoolname;
        private string _olschoolprovinceid;
        private string _olschoolcourseid;
        private string _olschoolquestionnum;
        private string _olschoolaomid;
        private string _olschoolmastertypeid;
        private string _olacccourseid;
        private string _isdel = "0";
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string Category
        {
            set { _category = value; }
            get { return _category; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolId
        {
            set { _olschoolid = value; }
            get { return _olschoolid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolName
        {
            set { _olschoolname = value; }
            get { return _olschoolname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolProvinceId
        {
            set { _olschoolprovinceid = value; }
            get { return _olschoolprovinceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolCourseId
        {
            set { _olschoolcourseid = value; }
            get { return _olschoolcourseid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolQuestionNum
        {
            set { _olschoolquestionnum = value; }
            get { return _olschoolquestionnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolAOMid
        {
            set { _olschoolaomid = value; }
            get { return _olschoolaomid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLSchoolMasterTypeId
        {
            set { _olschoolmastertypeid = value; }
            get { return _olschoolmastertypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OLAccCourseId
        {
            set { _olacccourseid = value; }
            get { return _olacccourseid; }
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
        #endregion Model
    }
}
