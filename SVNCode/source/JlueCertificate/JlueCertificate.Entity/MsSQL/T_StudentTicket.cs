using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public partial class T_StudentTicket
    {
        public T_StudentTicket()
        { }
        #region Model
        private long _id;
        private string _certificateid;
        private long _orgaizid;
        private string _studentid;
        private string _ticketnum;
        private string _serialnum;
        private string _olmobile;
        private string _isdel = "0";
        private DateTime _createtime = DateTime.Now;
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
        public long OrgaizId
        {
            set { _orgaizid = value; }
            get { return _orgaizid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StudentId
        {
            set { _studentid = value; }
            get { return _studentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TicketNum
        {
            set { _ticketnum = value; }
            get { return _ticketnum; }
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
        public string OLMobile
        {
            set { _olmobile = value; }
            get { return _olmobile; }
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
