using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;

namespace WebApplication3.EntityLayer.ViewModels
{
    public class ReservationViewModel
    {
        // This model was created to save the booking data provided by an user, 
        // while he is doing the booking process.
        public List<Passenger> RegistredPassengers { get; set; }
        public Flight SelectedFlight { get; set; }
        public Booking finalBooking { get; set; }

    }
}
