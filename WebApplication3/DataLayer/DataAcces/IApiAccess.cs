using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DataLayer.Api;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.DataLayer.DataAcces
{
    public interface IApiAccess
    {
        public string ApiUrl { get; set; }

        public List<Flight> SearchFlights(SearchFlightsViewModel filters,
            FiltersJsonObject queryApiJson);
    }
}
