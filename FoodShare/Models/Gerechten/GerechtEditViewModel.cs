using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class GerechtEditViewModel
    {
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public List<SelectListItem> Restaurant { get; set; }


    }
}
