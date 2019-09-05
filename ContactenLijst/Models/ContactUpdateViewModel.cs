using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactenLijst.Models
{
    public class ContactUpdateViewModel
    {
        
        [DisplayName("Voornaam")]
        [Required(ErrorMessage = "Voornaam is nodig")]
        public string Voornaam { get; set; }

        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "Achternaam is nodig")]
        [MinLength(5)]
        public string Naam { get; set; }

        [DisplayName("Geslacht")]
        public string Geslacht { get; set; }

        [DisplayName("Geboortedatum")]
        [Required(ErrorMessage = "Geboortedatum is nodig")]
        [DataType(DataType.DateTime)]
        public DateTime? Geboortedatum { get; set; }

        [DisplayName("E-mail adres")]
        [Required(ErrorMessage = "E-mail adres is nodig")]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string Email { get; set; }

        [DisplayName("Telefoonnummer")]
        [Required(ErrorMessage = "Telefoonnummer is nodig")]
        public string TelefoonNr { get; set; }

        [DisplayName("Adres")]
        public string Adres { get; set; }

        [DisplayName("Beschrijving")]
        public string Beschrijving { get; set; }

    }
}
