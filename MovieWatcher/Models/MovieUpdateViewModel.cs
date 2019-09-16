using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class MovieUpdateViewModel
    {
        public string Titel { get; set; }
        public string Genre { get; set; }
        public byte[] Foto { get; set; }
        public IFormFile newFoto { get; set; }
        public int Speeltijd { get; set; }


    }
}
