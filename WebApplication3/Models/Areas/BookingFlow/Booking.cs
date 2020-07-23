using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Models.Areas.BookingFlow
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Code { get; set; }
        public int ContactId { get; set; }
        public int FlightId { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Flight Flight { get; set; }

        public string GenerateCode()
        {
            StringBuilder genCode = new StringBuilder();
            Random rdm = new Random(System.DateTime.Now.Second);

            for(int i=0; i<3; i++)
            {
                genCode.Append((char)rdm.Next(65, 90));
            }

            for(int i=0; i<3; i++)
            {
                genCode.Append(rdm.Next(0, 9).ToString());
            }

            return genCode.ToString();
        }

        public Booking()
        {

        }
        public Booking(bool generateCode)
        {
            this.Code = GenerateCode();
        }
        //public Booking(string contactName, string contactEmail, string contactPhone, 
        //    List<Passenger> passengers, Flight selectedFlight)
        //{
        //    Contact = new Contact
        //    {
        //        Name = contactName,
        //        Email = contactEmail,
        //        Phone = contactPhone
        //    };
        //    Passengers = passengers;
        //    Flight = selectedFlight;
        //}
    }
}
