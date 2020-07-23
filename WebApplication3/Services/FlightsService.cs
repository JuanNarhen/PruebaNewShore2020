using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        // This service class is used to search the avaible flights in the api
        // (http://testapi.vivaair.com/otatest/api/values).

        private string _apiConnectString;

        public FlightsService (string apiConn)
        {
            this._apiConnectString = apiConn;
        }

        // This method creates the json string with the query data.
        // It receives the filters to search from the view-model and put the data into
        // FiltersJsonObject. This last object is serialized as a Json string.
        private string PrepareJson (SearchFlightsViewModel filters, FiltersJsonObject queryApiJson)
        {
            queryApiJson.Destination = filters.Destination;
            queryApiJson.Origin = filters.Origin;
            queryApiJson.From = filters.From.ToString("yyyy-MM-dd");

            string jsonFilters = JsonConvert.SerializeObject(queryApiJson);

            return jsonFilters;
        }

        // This method search the avaible flights in the avaible flights in the api an convert
        // the result (Json string) in a list of flights.
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

        // This is the principal method. It uses the two previous methods for search avaible flights in the api,
        // and if it finds return a list of flights, but if it doesn´t find any flight it throws a exception.
        public List<Flight> SearchFlights (SearchFlightsViewModel filters, 
            FiltersJsonObject queryApiJson, 
            IApi apiDb)
        {
            var jsonFilters = PrepareJson(filters, queryApiJson);
            dynamic result = apiDb.Post(this._apiConnectString, jsonFilters);

            if(result != null)
            {
                List<Flight> flights = GetFlights(result);
                bool isEmptyList = flights[0] == null;

                if (!isEmptyList)
                {
                    return flights;
                }
                else
                {
                    throw new Exception();
                }
                
            }
            else
            {
                throw new Exception();
            }

        }

    }
}
