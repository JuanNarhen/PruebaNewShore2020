using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication3.DataLayer.Api
{
    public class ApiFlight:IApi
    {
        // This method allows to do a POST type request to an API, and return an Dynamic tyep object
        // with the result.
        // Parameters: 'url':The API url.
        // 'json': The json string with the input data.
        // 'authorization': (Optional) When the API requires an authentication token you must input it.
        public dynamic Post(string url, string json, string authorization = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.Timeout = 30000;

                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (authorization != null)
                {
                    request.AddHeader("Authorization", authorization);
                }

                IRestResponse response = client.Execute(request);

                dynamic datos = JsonConvert.DeserializeObject(response.Content);

                return datos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
