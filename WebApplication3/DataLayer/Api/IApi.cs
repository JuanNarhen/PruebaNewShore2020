using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.DataLayer.Api
{
    // Interface of API´s connection for dependency injection
    public interface IApi
    {
        public dynamic Post(string url, string json, string autorizacion = null);
    }
}
