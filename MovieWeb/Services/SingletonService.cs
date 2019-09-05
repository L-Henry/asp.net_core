using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Services
{
    public class SingletonService : ISingletonService
    {

        private string _datum;
        public SingletonService()
        {
            _datum = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public string ToonDatum()
        {
            return _datum;
        }

    }
}
