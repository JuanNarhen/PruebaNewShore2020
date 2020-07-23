using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models.Areas.BookingFlow
{
    public interface IPerson
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
