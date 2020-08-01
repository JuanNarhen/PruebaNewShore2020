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
    public class BookingBL
    {
        private DBAcces _dbConn;

        public BookingBL(DBAcces connDb)
        {
            this._dbConn = connDb;
        }

        public void AddPassengerToBooking(string name, DateTime birth, ReservationViewModel bookingInfo)
        {
            bookingInfo.RegistredPassengers.Add(new Passenger { Name = name, BirthDate = birth });
        }

        public Booking CreateBooking(string contactName, string contactEmail, string contactPhone, ReservationViewModel bookingInfo)
        {
            Booking madeBooking = null;

            try
            {
                madeBooking = new Booking(true)
                {
                    Contact = new Contact
                    {
                        Name = contactName,
                        Email = contactEmail,
                        Phone = contactPhone
                    },
                    Passengers = bookingInfo.RegistredPassengers,
                    Flight = bookingInfo.SelectedFlight,
                };

                bookingInfo.finalBooking = madeBooking;

                return madeBooking;
            }
            catch (Exception e)
            {
                madeBooking = null;
                
                return madeBooking;
            }
        }

        public bool SaveBooking(ReservationViewModel bookingInfo)
        {
            try
            {
                // Verify if the flight exists in database for to avoid errors on repeated flights,
                // because the flight's table can not have the same two flights.. 
                
                bool exists = this._dbConn.FlightExists(bookingInfo.SelectedFlight);

                if (!exists)
                {
                    SaveWithFlight(bookingInfo);
                    return true;
                }
                else
                {
                    SaveWithoutFlight(bookingInfo);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // Save booking with non-existent flight
        private void SaveWithFlight(ReservationViewModel bookingInfo)
        {
            this._dbConn.SaveBooking(bookingInfo.finalBooking);
        }

        // Save booking with existent flight
        private void SaveWithoutFlight(ReservationViewModel bookingInfo)
        {
            bookingInfo.finalBooking.FlightId = bookingInfo.SelectedFlight.FlightNumber;
            bookingInfo.finalBooking.Flight = null;
            this._dbConn.SaveBooking(bookingInfo.finalBooking);
        }

        public Booking FindBooking(string code)
        {
            var booking = this._dbConn.FindBooking(code);

            return booking;
        }

    }
}
