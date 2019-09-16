using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class MovieCreateViewModel
    {
        public string Titel { get; set; }
        public List<GenreListViewModel> GenreList { get; set; }

        public IFormFile Foto { get; set; }
        public int Speeltijd { get; set; }


    }
}
