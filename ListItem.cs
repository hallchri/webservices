using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBookingApp
{
    public class ListItem
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string id { get; set; }
        public string OtherValues { get; set; }
        public override string ToString()
        {
            return Number + Name + id + OtherValues;
        }
    }
}
