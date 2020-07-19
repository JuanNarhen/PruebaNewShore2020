﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models.ViewModels;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class BookingFlowController : Controller
    {

        [HttpGet]
        public IActionResult Index ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index (SearchFlightsViewModel filters)
        {
            FlightsService service = new FlightsService("http://testapi.vivaair.com/otatest/api/values");
            FiltersJsonObject queryJson = new FiltersJsonObject();
            IApi queryFlights = new ApiFlight();
            try
            {
                filters.Flights = service.SearchFlights(filters, queryJson, queryFlights);
                return View(filters);
            }
            catch(Exception e)
            {
                ViewBag.Exception = "No existen vuelos disponibles.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ContinueReservation (string departure, 
            string arrival, 
            DateTime departureDate)
        {
            return View();
        }

        

    }
}