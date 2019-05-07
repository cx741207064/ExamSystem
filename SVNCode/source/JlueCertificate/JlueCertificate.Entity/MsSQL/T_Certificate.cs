using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public partial class T_Certificate
    {
        public T_Certificate()
        { }
        #region Model
        private long _id;
        private string _categoryname;
        private string _examsubject;
        private DateTime _starttime;
        private DateTime _endtime;
        private string _normalresult;
        private string _examresult;
        private string _rule = "O";
        private string _describe;
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
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamSubject
        {
            set { _examsubject = value; }
            get { return _examsubject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
        /// O（order:顺序），D（disorder：无顺序，单独）
        /// </summary>
        public string Rule
        {
            set { _rule = value; }
            get { return _rule; }
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
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}
