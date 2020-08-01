using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.EntityLayer.Areas.BookingFlow
{
    public class Flight
    {
        public int ID { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public int FlightNumber { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }

        public override bool Equals(object obj)
        {
            try
            {
                var fobj = (Flight)obj;

                bool equals =
                    this.DepartureStation == fobj.DepartureStation &&
                    this.ArrivalStation == fobj.ArrivalStation &&
                    this.DepartureDate == fobj.DepartureDate &&
                    this.FlightNumber == fobj.FlightNumber;

                return equals;
            }
            catch(Exception e)
            {
                return false;
            }
        }

    }
}
