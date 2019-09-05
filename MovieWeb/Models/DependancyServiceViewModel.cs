using MovieWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Models
{
    public class DependancyServiceViewModel
    {
        public IScopedService Scoped { get; set; }
        public ISingletonService Singelton { get; set; }
        public ITransientService Transient { get; set; }

    }
}
