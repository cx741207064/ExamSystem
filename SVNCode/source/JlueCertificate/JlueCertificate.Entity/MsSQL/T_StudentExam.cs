using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_StudentExam
    {
        public T_StudentExam()
        { }
        #region Model
        private string _examid;
        private long _studentid;
        private long _schemeid;
        private long _organizaid;
        private int? _paytype;
        private int _isdel = 0;
        private DateTime _createtime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public string ExamId
        {
            set { _examid = value; }
            get { return _examid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long StudentId
        {
            set { _studentid = value; }
            get { return _studentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long SchemeId
        {
            set { _schemeid = value; }
            get { return _schemeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long OrganizaId
        {
            set { _organizaid = value; }
            get { return _organizaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsDel
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
