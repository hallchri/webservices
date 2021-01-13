using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp
{
    class Booking
    {
        public int id { get; set; }
        public int roomId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
