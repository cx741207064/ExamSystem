using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_CertifiScheme
    {
        public T_CertifiScheme()
        { }
        #region Model
        private long _id;
        private long _certificateid;
        private long _certifisubjectid;
        private string _name;
        private string _abbname;
        private string _describe;
        private string _isdel = "0";
        private DateTime _createdtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public long Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CertificateId
        {
            set { _certificateid = value; }
            get { return _certificateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long CertifiSubjectId
        {
            set { _certifisubjectid = value; }
            get { return _certifisubjectid; }
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
        public string AbbName
        {
            set { _abbname = value; }
            get { return _abbname; }
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
        public string IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatedTime
        {
            set { _createdtime = value; }
            get { return _createdtime; }
        }
        #endregion Model

    }
}
