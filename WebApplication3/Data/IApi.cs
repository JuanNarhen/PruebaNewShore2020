using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Data
{
    public interface IApi
    {
        public dynamic Post(string url, string json, string autorizacion = null);
    }
}
