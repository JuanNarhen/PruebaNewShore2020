using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Areas.BookingFlow
{
    public class Contact : IPerson
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

    }
}
