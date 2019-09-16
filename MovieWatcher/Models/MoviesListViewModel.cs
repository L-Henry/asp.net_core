using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class MoviesListViewModel
    {

        public int Id { get; set; }
        public string Titel { get; set; }
        public List<string> Genres { get; set; }
        public int Rating { get; set; }
        public int AantalGezien { get; set; }
        public List<SelectListItem> HeeftGezienSelectList { get; set; }
        public int HeeftGezien { get; set; }

    }
}
