using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Areas.BookingFlow;

namespace WebApplication3.Models.ViewModels
{
    public class ReservationViewModel
    {

        public List<Passenger> RegistredPassengers { get; set; }
        public Flight SelectedFlight { get; set; }
        public Booking finalBooking { get; set; }

    }
}
