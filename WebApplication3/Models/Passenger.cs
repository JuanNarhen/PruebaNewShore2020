using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Passenger : IPerson
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

    }
}
