using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]
    public class T_Organiza
    {
        public T_Organiza()
        { }
        #region Model
        private long _id;
        private string _name;
        private string _password;
        private string _appname;
        private string _classid;
        private string _path;
        private string _describe;
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
        public string AppName
        {
            set { _appname = value; }
            get { return _appname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClassId
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
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
