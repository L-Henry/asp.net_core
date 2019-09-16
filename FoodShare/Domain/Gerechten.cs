using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Domain
{
    public class Gerechten
    {
        public int Id { get; set; }
        public string Gerecht { get; set; }
        public decimal Prijs { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }
    }
}
