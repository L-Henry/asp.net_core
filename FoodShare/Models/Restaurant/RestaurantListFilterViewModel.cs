using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class RestaurantListFilterViewModel
    {
        public IEnumerable<RestaurantListViewModel> Restaurants { get; set; }
        public List<SelectListItem> Types { get; set; }
        public List<SelectListItem> IsNogOpen { get; set; }

    }
}
