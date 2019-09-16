using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class GerechtListFilerViewModel
    {
        public IEnumerable<GerechtListViewModel> Gerechten { get; set; }
        public string Naam { get; set; }
        public List<SelectListItem> IsNogOpen { get; set; }
        public bool ReedsBesteld { get; set; }
        public int AantalKeerbesteld { get; set; }


    }
}
