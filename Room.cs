using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp
{
    class Room
    {
        // Dapper-extension kräver att det finns get och set metoder på våra variabler
        public int id { get; set; } // förkortad verison av get och set-metod (så som i datastrukturer och algoritmer kursen)
        public string name { get; set; }
        public int seats { get; set; }
        public int computers { get; set; }
    }
}
