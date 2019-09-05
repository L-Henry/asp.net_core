using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Services
{
    public class TransientService : ITransientService
    {
        private string _datum;
        public TransientService()
        {
            _datum = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public string ToonDatum()
        {
            return _datum;
        }
    }
}
