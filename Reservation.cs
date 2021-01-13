using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingApp
{
    public class Reservation
    {
        public string CabinName { get; set; }
        public string ServiceType { get; set; }
        public DateTime ReservationDate { get; set; } // blir kanski ett problem me att sätt in i databasen om he int e rätt format (YYYY-MM-DD)
        public int id { get; set; }
        public string ownerId { get; set; }
    }
}
