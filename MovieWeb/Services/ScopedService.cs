using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Services
{
    public class ScopedService : IScopedService
    {
        private string _datum;
        public ScopedService()
        {
            _datum =  DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public string ToonDatum()
        {
            return _datum;
        }

    }
}
