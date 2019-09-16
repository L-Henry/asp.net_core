using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class RestaurantListViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public bool ReedsVanBesteld { get; set; }
        public int AantalKeerBijBesteld { get; set; }
        public decimal GemiddeldePrijs { get; set; }

    }
}
