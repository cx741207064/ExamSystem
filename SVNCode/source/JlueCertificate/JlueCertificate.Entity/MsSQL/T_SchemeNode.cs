using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_SchemeNode
    {
        public T_SchemeNode()
        { }
        #region Model
        private long _id;
        private long _schemeid;
        private int? _type;
        private int _level = 1;
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
        public long SchemeId
        {
            set { _schemeid = value; }
            get { return _schemeid; }
        }
        /// <summary>
        /// 1:好会计电脑账, 2: 报税 
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            set { _level = value; }
            get { return _level; }
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
