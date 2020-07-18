using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Areas.BookingFlow;

namespace WebApplication3.Models.ViewModels
{
    public class SearchFlightsViewModel
    {
        public string Departure { get; set; }
        public string Arrivation { get; set; }
        public DateTime FlightDateTime { get; set; }
        public List<Flight> AvaibleFlights { get; set; }

    }
}
