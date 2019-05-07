using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_ExamSchedule
    {
        public T_ExamSchedule()
        { }
        #region Model
        private long _id;
        private long _examid;
        private long _nodeid;
        private int? _status;
        private DateTime? _starttime;
        private DateTime? _endtime;
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
        public long ExamId
        {
            set { _examid = value; }
            get { return _examid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long NodeId
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
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
