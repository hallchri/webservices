using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRoomBookingApi.Models
{
    public class User
    {
        public string _id { get; set; }
        public string email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public int __v { get; set; }

    }

    public class CabinRoot
    {
        public User User { get; set; }
    }
}
