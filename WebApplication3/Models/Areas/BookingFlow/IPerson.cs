using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Areas.BookingFlow
{
    public interface IPerson
    {
        // The "ID" property works like primary key in the database for the entities that implement this
        // interface
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
