using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.BusinessLayer
{
    public interface IBookingBL
    {
        public void AddPassengerToBooking(string name, DateTime birth, ReservationViewModel bookingInfo);
        public Booking CreateBooking(string contactName, string contactEmail,
            string contactPhone, ReservationViewModel bookingInfo);
        public bool SaveBooking(ReservationViewModel bookingInfo);
        public Booking FindBooking(string code);


    }
}
