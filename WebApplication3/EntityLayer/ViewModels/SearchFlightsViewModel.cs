using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;

namespace WebApplication3.EntityLayer.ViewModels
{
    public class SearchFlightsViewModel
    {
        public string Destination { get; set; }
        public string Origin { get; set; }
        public DateTime From { get; set; }
        public List<Flight> Flights { get; set; }

    }
}
