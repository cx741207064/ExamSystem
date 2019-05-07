using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    public class T_MarkUser
    {
        public T_MarkUser()
        { }
        #region Model
        private long _id;
        private string _name;
        private string _password;
        private string _level;
        private int _isdel = 0;
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
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Level
        {
            set { _level = value; }
            get { return _level; }
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
