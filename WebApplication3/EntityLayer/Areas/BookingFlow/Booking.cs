using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.EntityLayer.Areas.BookingFlow
{
    public class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Code { get; set; }

        // The property "ContactId" links the booking´s contact in the database
        public int ContactId { get; set; }

        // The property "FlightId" links the booking´s flight in the database
        public int FlightId { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Flight Flight { get; set; }

        //Method for generate a random code for the booking
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

        //This empty constructor is for the entity framework, to build the objects in a query
        public Booking()
        {

        }

        //This constructor is for to generate a new booking with its own code created.
        public Booking(bool generateCode)
        {
            this.Code = GenerateCode();
        }
    }
}
