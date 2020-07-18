﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
    public class Flight
    {
        public string DepartureStation { get; set; }
        public string ArrivationStation { get; set; }
        public DateTime DepartureDate { get; set; }
        public int FlightNumber { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        
    }
}
