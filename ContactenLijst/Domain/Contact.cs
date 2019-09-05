using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactenLijst.Domain
{
    public class Contact
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Geslacht { get; set; }
        public DateTime? Geboortedatum { get; set; }
        public string Email { get; set; }
        public string TelefoonNr { get; set; }
        public string Adres { get; set; }
        public string Beschrijving { get; set; }
        public byte[] Avatar { get; set; }
    }
}
