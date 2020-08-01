using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DataLayer.Api;
using WebApplication3.EntityLayer.Areas.BookingFlow;
using WebApplication3.EntityLayer.ViewModels;

namespace WebApplication3.DataLayer.DataAcces
{
    public class ApiAccess
    {
        private IApi _apiConn;

        // the deafualt url is (http://testapi.vivaair.com/otatest/api/values) for avaible flights
        public string ApiUrl { get; set; }

        public ApiAccess(IApi conn)
        {
            this._apiConn = conn;
        }

        #region Flight

        // This method creates the json string with the query data.
        // It receives the filters to search from the view-model and put the data into
        // FiltersJsonObject. This last object is serialized as a Json string.
        private string PrepareJson(SearchFlightsViewModel filters, FiltersJsonObject queryApiJson)
        {
            queryApiJson.Destination = filters.Destination;
            queryApiJson.Origin = filters.Origin;
            queryApiJson.From = filters.From.ToString("yyyy-MM-dd");

            string jsonFilters = JsonConvert.SerializeObject(queryApiJson);

            return jsonFilters;
        }

        // This method search the avaible flights in the avaible flights in the api an convert
        // the result (Json string) in a list of flights.
        private List<Flight> GetFlights(dynamic jsonResult)
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
        public List<Flight> SearchFlights(SearchFlightsViewModel filters,
            FiltersJsonObject queryApiJson)
        {
            var jsonFilters = PrepareJson(filters, queryApiJson);
            dynamic result = this._apiConn.Post(this.ApiUrl, jsonFilters);
            List<Flight> flights = null;

            if (result != null)
            {
                flights = GetFlights(result);
                bool isEmptyList = flights[0] == null;

                if (!isEmptyList)
                {
                    return flights;
                }
                else
                {
                    flights = null;
                }
            }

            return flights;
        }

        #endregion
    }
}
