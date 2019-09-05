using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWeb.Models
{
    public class MovieCreateViewModel
    {
        [DisplayName("Titel")]
        [Required(ErrorMessage ="Titel is nodig")]
        public string Title { get; set; }


        [DisplayName("Omschrijving")]
        [Required(ErrorMessage = "Beschrijving is nodig")]
        [MinLength(5)]
        public string Description { get; set; }


        [DisplayName("Score")]
        [Range(0,10)]
        public double Rating { get; set; }


        [DisplayName("Genre")]
        public string Genre { get; set; }


        [DisplayName("Uitbrengdatum")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
    }
}
