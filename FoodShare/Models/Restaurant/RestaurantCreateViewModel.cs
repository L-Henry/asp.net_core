using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class RestaurantCreateViewModel
    {
        public string Naam { get; set; }
        public List<SelectListItem> TypeList { get; set; }
        public int TypeId { get; set; }


    }
}
