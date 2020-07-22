using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models.Areas.BookingFlow;
using WebApplication3.Models.ViewModels;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class BookingFlowController : Controller
    {
        private static ReservationViewModel _bookingInfo;

        [HttpGet]
        public IActionResult Index ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index (SearchFlightsViewModel filters)
        {
            FlightsService service = new FlightsService("http://testapi.vivaair.com/otatest/api/values");
            FiltersJsonObject queryJson = new FiltersJsonObject();
            IApi queryFlights = new ApiFlight();
            try
            {
                filters.Flights = service.SearchFlights(filters, queryJson, queryFlights);
                return View(filters);
            }
            catch(Exception e)
            {
                ViewBag.Exception = "No existen vuelos disponibles.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ContinueReservation (string departure, 
            string arrival, 
            DateTime departureDate,
            int number,
            float price,
            string currency)
        {

            _bookingInfo = new ReservationViewModel
            {
                SelectedFlight = new Flight()
                {
                    ArrivalStation = arrival,
                    DepartureStation = departure,
                    DepartureDate = departureDate,
                    FlightNumber = number,
                    Price = price,
                    Currency = currency
                },
                RegistredPassengers = new List<Passenger>()
            };

            return View(_bookingInfo);
        }

        [HttpGet]
        public PartialViewResult AddPassenger(string name, DateTime date)
        {

            _bookingInfo.RegistredPassengers.Add(new Passenger { Name = name, BirthDate = date });

            return PartialView("_SeePassenger",_bookingInfo.RegistredPassengers);

        }

        [HttpGet]
        public IActionResult FinalizeReservation(string contactName, string contactEmail, string contactPhone)
        {
            Booking madeBooking = new Booking()
            {
                BookingContact = new Contact
                {
                    Name = contactName,
                    Email = contactEmail,
                    Phone = contactPhone
                },
                Passengers = _bookingInfo.RegistredPassengers,
                BookingFlight = _bookingInfo.SelectedFlight,
            };
            return View(madeBooking);
        }
    }
}