using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class MoviesFilterListViewModel
    {
        public IEnumerable<MoviesListViewModel> Movies { get; set; }
        public string Titel { get; set; }
        public int GezienStatus { get; set; }
        public int Rating { get; set; }
        public int Genre { get; set; }
        public bool Review { get; set; }
    }
}
