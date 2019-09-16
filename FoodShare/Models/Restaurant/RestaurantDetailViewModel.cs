using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class RestaurantDetailViewModel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<GerechtListViewModel> Gerechten { get; set; }


    }
}
