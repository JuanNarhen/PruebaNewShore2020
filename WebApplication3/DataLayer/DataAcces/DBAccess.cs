using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DataLayer.AppDB;
using WebApplication3.EntityLayer.Areas.BookingFlow;

namespace WebApplication3.DataLayer.DataAcces
{
    public class DBAccess : IDBAccess
    {
        private AppDbContext _dbConn;

        public DBAccess(AppDbContext conn)
        {
            this._dbConn = conn;
        }

        #region Flight

        public bool FlightExists(Flight flight)
        {
            return this._dbConn.Flights.Contains(flight);
        }

        #endregion

        #region Booking
        public bool SaveBooking(Booking booking)
        {
            try
            {
                this._dbConn.Bookings.Add(booking);
                this._dbConn.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public Booking FindBooking(string bookingCode)
        {
            var booking = this._dbConn.Bookings.Find(bookingCode);

            if(booking != null)
            {
                this._dbConn.Entry(booking).Collection(x => x.Passengers).Load();
                this._dbConn.Entry(booking).Reference(x => x.Contact).Load();
                this._dbConn.Entry(booking).Reference(x => x.Flight).Load();
            }

            return booking;
        }

        #endregion
    }
}
