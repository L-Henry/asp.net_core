using ContactenLijst.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContactenLijst.Models
{
    public class ContactCreateViewModel
    {
        [DisplayName("Voornaam")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Geen cijfers toegelaten.")]
        [Required(ErrorMessage = "Voornaam is nodig")]
        public string Voornaam { get; set; }

        [DisplayName("Achternaam")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Geen cijfers toegelaten.")]
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
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Geef geldig e-mail adres in.")]
        //[DataType(DataType)]
        public string Email { get; set; }

        [DisplayName("Telefoonnummer")]
        [Required(ErrorMessage = "Telefoonnummer is nodig")]
        [Phone(ErrorMessage = "Geef geldig telefoonnummer in.")]
        public string TelefoonNr { get; set; }

        [DisplayName("Adres")]
        public string Adres { get; set; }

        [DisplayName("Beschrijving")]
        public string Beschrijving { get; set; }

        public IFormFile Avatar { get; set; }

    }
}
