using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Models.Areas.BookingFlow
{
    public class Booking
    {
        public string Code { get; set; }
        public List<Passenger> Passengers { get; set; }
        public Contact BookingContact { get; set; }
        public Flight BookingFlight { get; set; }

        public string GenerateCode()
        {
            StringBuilder genCode = new StringBuilder();
            genCode.Append("ABC");
            Random rdm = new Random(System.DateTime.Now.Second);

            for(int i=0; i<3; i++)
            {
                genCode.Append(rdm.Next(0,9).ToString());
            }

            return genCode.ToString();
        }

        public Booking()
        {
            this.Code = GenerateCode();
        }
    }
}
