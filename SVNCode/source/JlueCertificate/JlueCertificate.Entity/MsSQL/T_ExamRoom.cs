using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public class T_ExamRoom
    {
        public T_ExamRoom()
        { }
        #region Model
        private int _id;
        private string _examname;
        private string _examplace;
        private string _centrename;
        private string _examnum;
        private string _seatnum;
        private DateTime _resultreleasetime;
        private DateTime _createtime = DateTime.Now;
        private string _isdel = "0";
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamName
        {
            set { _examname = value; }
            get { return _examname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamPlace
        {
            set { _examplace = value; }
            get { return _examplace; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CentreName
        {
            set { _centrename = value; }
            get { return _centrename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ExamNum
        {
            set { _examnum = value; }
            get { return _examnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SeatNum
        {
            set { _seatnum = value; }
            get { return _seatnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ResultReleaseTime
        {
            set { _resultreleasetime = value; }
            get { return _resultreleasetime; }
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
        public string IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        #endregion Model

    }
}
