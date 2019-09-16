using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class GerechtListViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public string Restaurant { get; set; }

    }
}
