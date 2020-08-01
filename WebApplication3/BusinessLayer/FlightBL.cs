using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DataLayer.Api;
using WebApplication3.DataLayer.DataAcces;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.BusinessLayer
{
    public class FlightBL : IFlightBL
    {
        private IApiAccess _apiConn;

        public FlightBL(IApiAccess connApi)
        {
            this._apiConn = connApi;
            this._apiConn.ApiUrl = "http://testapi.vivaair.com/otatest/api/values";
        }

        public IEnumerable<Flight> SearchAvaibleFlights(SearchFlightsViewModel filters)
        {
            var jsonFilters = new FiltersJsonObject();

            IEnumerable<Flight> result =
                this._apiConn.SearchFlights(filters, jsonFilters).ToImmutableList();

            return result;
        }

    }
}
