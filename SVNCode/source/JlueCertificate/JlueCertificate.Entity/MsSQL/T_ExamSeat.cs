using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.MsSQL
{
    [Serializable]

    public class T_ExamSeat
    {
        public T_ExamSeat()
        { }
        #region Model
        private int _id;
        private string _examroomid;
        private string _seatnumber;
        private string _ticketid;
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
        public string ExamRoomId
        {
            set { _examroomid = value; }
            get { return _examroomid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SeatNumber
        {
            set { _seatnumber = value; }
            get { return _seatnumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TicketId
        {
            set { _ticketid = value; }
            get { return _ticketid; }
        }

        #endregion Model
    }
}
