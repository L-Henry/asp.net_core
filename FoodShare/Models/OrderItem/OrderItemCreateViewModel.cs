using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class OrderItemCreateViewModel
    {
        public List<SelectListItem> Gerechten { get; set; }

        public int Aantal { get; set; }

    }
}
