using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ProjectUpdateViewModel
    {
        [DisplayName("Titel")]
        [Required(ErrorMessage = "Titel is nodig")]
        public string Titel { get; set; }

        [DisplayName("Beschrijving")]
        [Required(ErrorMessage = "Beschrijving is nodig")]
        [MinLength(5), MaxLength(1000)]
        public string Beschrijving { get; set; }

        public int Status { get; set; }
    
        public byte[] Image { get; set; }

        [Required]
        public string Tags { get; set; }

        public IFormFile newImage { get; set; }
    }
}
