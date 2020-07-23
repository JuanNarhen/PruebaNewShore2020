using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Entity;
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
        private AppDbContext _dbConn;
        private static ReservationViewModel _bookingInfo;

        public BookingFlowController(AppDbContext dbConn)
        {
            this._dbConn = dbConn;
        }

        [HttpGet]
        public IActionResult Index ()
        {
            return View();
        }

        // This method search the avaible flights and return them in a list
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
            _bookingInfo.RegistredPassengers.Add(new Passenger { Name = name, BirthDate = date });

            return PartialView("_SeePassenger",_bookingInfo.RegistredPassengers);
        }

        // This method finalize the booking with the contact data and passengers data.
        [HttpGet]
        public IActionResult FinalizeReservation(string contactName, string contactEmail, string contactPhone)
        {
            Booking madeBooking = new Booking(true)
            {
                Contact = new Contact
                {
                    Name = contactName,
                    Email = contactEmail,
                    Phone = contactPhone
                },
                Passengers = _bookingInfo.RegistredPassengers,
                Flight = _bookingInfo.SelectedFlight,
            };

            _bookingInfo.finalBooking = madeBooking;

            return View(madeBooking);
        }

        // This is the final-order method used. It saves the booking "_bookingInfo" in the DB.
        [HttpGet]
        public IActionResult SaveReservation()
        {
            try
            {
                // Verify if the flight exists in database for to avoid errors on repeated flights,
                // because the flight's table can not have the same two flights.. 
                int fn = _bookingInfo.SelectedFlight.FlightNumber;
                var flight = _dbConn.Flights.Find(fn);
                bool exists = flight != null;

                if (!exists)
                {
                    SaveWithFlight();
                    return View("Index");
                }
                else
                {
                    SaveWithoutFlight();
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.SaveError = e.Message + "\n" + e.StackTrace + "\n";
                return View("Index");
            }
        }

        // Save booking with non-existent flight
        private void SaveWithFlight()
        {
            this._dbConn.Bookings.Add(_bookingInfo.finalBooking);
            this._dbConn.SaveChanges();
        }

        // Save booking with existent flight
        private void SaveWithoutFlight()
        {
            _bookingInfo.finalBooking.FlightId = _bookingInfo.SelectedFlight.FlightNumber;
            _bookingInfo.finalBooking.Flight = null;
            this._dbConn.Add(_bookingInfo.finalBooking);
            this._dbConn.SaveChanges();
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
            Booking booking = this._dbConn.Bookings.Find(code);
            bool isNotNull = booking != null;

            if (isNotNull)
            {
                // Load booking components from database
                this._dbConn.Entry(booking).Collection(x => x.Passengers).Load();
                this._dbConn.Entry(booking).Reference(x => x.Contact).Load();
                this._dbConn.Entry(booking).Reference(x => x.Flight).Load();

                return View(booking);
            }

            ViewBag.NullBooking = "La reserva especificada no existe en la base de datos.";

            return View();
        }
    }
}