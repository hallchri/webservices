using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoomBookingApi.Models
{
    public class Booking
    {
        public int id { get; set; }
        public int roomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
