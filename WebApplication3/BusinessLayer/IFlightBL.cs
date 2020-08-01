using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.BusinessLayer
{
    public interface IFlightBL
    {
        public IEnumerable<Flight> SearchAvaibleFlights(SearchFlightsViewModel filters);
    }
}
