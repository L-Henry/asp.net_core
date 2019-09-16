using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Domain
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public RestaurantType Type { get; set; }
        public int TypeId { get; set; }
        public ICollection<Gerechten> Gerechten { get; set; }
        public bool NogOpen { get; set; } = true;

    }
}
