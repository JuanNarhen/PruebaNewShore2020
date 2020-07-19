using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;
using WebApplication3.Models.Areas.BookingFlow;
using WebApplication3.Models.ViewModels;

namespace WebApplication3.Services
{
    public class FlightsService
    {

        private string _apiConnectString;

        public FlightsService (string apiConn)
        {
            this._apiConnectString = apiConn;
        }

        private string PrepareJson (SearchFlightsViewModel filters, FiltersJsonObject queryApiJson)
        {
            queryApiJson.Destination = filters.Destination;
            queryApiJson.Origin = filters.Origin;
            queryApiJson.From = filters.From.ToString("yyyy-MM-dd");

            string jsonFilters = JsonConvert.SerializeObject(queryApiJson);

            return jsonFilters;
        }

        private List<Flight> GetFlights (dynamic jsonResult)
        {
            List<Flight> flights = new List<Flight>();
            Object[] jsonArray = JsonConvert.DeserializeObject<Object[]>(jsonResult);

            foreach (var i in jsonArray)
            {
                Flight f = JObject.Parse(i.ToString()).ToObject<Flight>();
                flights.Add(f);
            }

            return flights;
        }

        public List<Flight> SearchFlights (SearchFlightsViewModel filters, 
            FiltersJsonObject queryApiJson, 
            IApi apiDb)
        {
            var jsonFilters = PrepareJson(filters, queryApiJson);
            dynamic result = apiDb.Post(this._apiConnectString, jsonFilters);

            if(result != null)
            {
                List<Flight> flights = GetFlights(result);
                return flights;
            }
            else
            {
                throw new Exception("There is an error with the query to API.\nIt returns a null value");
            }

        }

    }
}
