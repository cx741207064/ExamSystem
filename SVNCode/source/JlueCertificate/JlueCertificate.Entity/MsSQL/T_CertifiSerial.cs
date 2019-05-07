using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public partial class T_CertifiSerial
    {
        public T_CertifiSerial()
        { }
        #region Model
        private long _id;
        private string _certificateid;
        private string _serialnum;
        private string _state;
        private string _isdel = "0";
        private DateTime _createtime = DateTime.Now;
        private DateTime? _issuedate;
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
        public string CertificateId
        {
            set { _certificateid = value; }
            get { return _certificateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SerialNum
        {
            set { _serialnum = value; }
            get { return _serialnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
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
        public DateTime? IssueDate
        {
            set { _issuedate = value; }
            get { return _issuedate; }
        }
        #endregion Model

    }
}
