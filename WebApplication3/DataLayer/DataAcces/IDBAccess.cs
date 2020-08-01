using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;

namespace WebApplication3.DataLayer.DataAcces
{
    public interface IDBAccess
    {
        public bool FlightExists(Flight flight);
        public bool SaveBooking(Booking booking);
        public Booking FindBooking(string bookingCode);

    }
}
