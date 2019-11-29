using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JlueCertificate.Entity.Request
{
    [Serializable]
    public class addexamseat
    {
        public int id { get; set; }
        public string ExamRoomId { get; set; }
        public string SeatNumber { get; set; }
        public string TicketId { get; set; }
    }
}
