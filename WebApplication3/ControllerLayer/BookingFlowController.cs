using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.BusinessLayer;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.ControllerLayer
{
    public class BookingFlowController : Controller
    {
        private static ReservationViewModel _bookingInfo;
        private BookingBL _bookingBL;
        private FlightBL _flightBL;

        public BookingFlowController(BookingBL bookingBL, FlightBL flightBL)
        {
            this._bookingBL = bookingBL;
            this._flightBL = flightBL;
        }

        [HttpGet]
        public IActionResult Index ()
        {
            return View();
        }

        // This method search the avaible flights and return them in a list
        [HttpPost]
        public IActionResult Index(SearchFlightsViewModel filters)
        {
            IEnumerable<Flight> avaibleFlights =
                this._flightBL.SearchAvaibleFlights(filters);

            if (avaibleFlights != null)
            {
                filters.Flights = avaibleFlights.ToList();
                return View(filters);
            }
            else
            {
                ViewBag.NotAvaible = "No existen vuelos disponibles.";
                return View(filters);
            }
        }

        // This method initialize the booking´s flight contained in "_bookingInfo", and provides
        // the View with forms for the contact data and passengers data
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

        // Method for add passengers to the booking.
        [HttpGet]
        public PartialViewResult AddPassenger(string name, DateTime date)
        {
            //_bookingInfo.RegistredPassengers.Add(new Passenger { Name = name, BirthDate = date });
            this._bookingBL.AddPassengerToBooking(name, date, _bookingInfo);

            return PartialView("_SeePassenger",_bookingInfo.RegistredPassengers);
        }

        // This method finalize the booking with the contact data and passengers data.
        [HttpGet]
        public IActionResult FinalizeReservation(string contactName, string contactEmail, string contactPhone)
        {
            var finalBooking = this._bookingBL.CreateBooking(contactName, contactEmail, contactPhone, _bookingInfo);

            return View(finalBooking);
        }

        // This is the final-order method used. It saves the booking "_bookingInfo" in the DB.
        [HttpGet]
        public IActionResult SaveReservation()
        {
            bool saved = this._bookingBL.SaveBooking(_bookingInfo);

            if (!saved)
            {
                ViewBag.SaveError = "No se ha podido guardar su reserva.";
            }

            return View("Index");
        }

        // The next two methods are used to search a booking.
        [HttpGet]
        public IActionResult SearchReservation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchReservation(string code)
        {
            var booking = this._bookingBL.FindBooking(code);
            bool isNotNull = booking != null;

            if (isNotNull)
            {
                return View(booking);
            }

            ViewBag.NullBooking = "La reserva especificada no existe en la base de datos.";

            return View();
        }
    }
}