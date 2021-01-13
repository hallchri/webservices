using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoomBookingApi.Models
{
    public class Room
    {
        public int id { get; set; }
        public string name { get; set; }
        public int seats { get; set; }
        public int computers { get; set; }
    }
}
