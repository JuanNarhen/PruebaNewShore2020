using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;
using WebApplication3.Models.Areas.BookingFlow;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Services
{
    public class FlightsService
    {

        private List<Flight> GetFlights(dynamic JsonResult)
        {
            List<Flight> flights = new List<Flight>();

            foreach (var i in JsonResult)
            {
                Flight f = new Flight();
                f.DepartureStation = i.DepartureStation.ToString();
                f.ArrivalStation = i.ArrivalStation.ToString();
                f.Currency = i.Currency.ToString();
                f.Price = float.Parse(i.Price.ToString());
                f.DepartureDate = Convert.ToDateTime(i.DepartureDate);

                flights.Add(f);
            }

            return flights;
        }

        public void SearchFlights (SearchFlightsViewModel filters)
        {
            bool byDeparture = String.IsNullOrEmpty(filters.Departure);
            bool byArrivation = String.IsNullOrEmpty(filters.Arrivation);
            bool byDate = filters.FlightDateTime == null;

            if (byDeparture)
            {
                
            }
        }
    }
}
