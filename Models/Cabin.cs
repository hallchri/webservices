using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoomBookingApi.Models
{
    public class Cabin
    {

        public string _id       { get; set; }
        public string owner     { get; set; }
        public string adress    { get; set; }
        public int m2           { get; set; }
        public string sauna     { get; set; }
        public string beach     { get; set; }
        public string ownerId   { get; set; }
    }

    public class RootClass
    {
        public Cabin Cabin { get; set; }
    }
}
