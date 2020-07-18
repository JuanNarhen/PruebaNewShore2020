using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Booking
    {
        public string Code { get; set; }
        public List<Passenger> Passengers { get; set; }
        public Contact BookingContact { get; set; }
        public Flight BookingFlight { get; set; }

    }
}
