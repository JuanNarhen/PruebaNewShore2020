using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.DataLayer.Api
{
    // Model with filters to search the avaible flights
    public class FiltersJsonObject
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string From { get; set; }
    }
}
