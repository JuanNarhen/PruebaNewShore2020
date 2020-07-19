using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models.Areas.BookingFlow;

namespace WebApplication3.Models.ViewModels
{
    public class SearchFlightsViewModel
    {
        public string Destination { get; set; }
        public string Origin { get; set; }
        public DateTime From { get; set; }

    }
}
