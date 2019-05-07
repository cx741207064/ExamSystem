using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_CertifiSubject
    {
        public T_CertifiSubject()
        { }
        #region Model
        private long _id;
        private string _certificateid;
        private string _subjectid;
        private string _normalresult;
        private string _examresult;
        private string _isneedexam = "0";
        private long? _examlength;
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
        public string CertificateId
        {
            set { _certificateid = value; }
            get { return _certificateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubjectId
        {
            set { _subjectid = value; }
            get { return _subjectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NormalResult
        {
            set { _normalresult = value; }
            get { return _normalresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamResult
        {
            set { _examresult = value; }
            get { return _examresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IsNeedExam
        {
            set { _isneedexam = value; }
            get { return _isneedexam; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? ExamLength
        {
            set { _examlength = value; }
            get { return _examlength; }
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
