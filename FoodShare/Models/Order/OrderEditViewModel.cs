using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class OrderEditViewModel
    {

        public List<SelectListItem> Restaurant { get; set; }
        public decimal Leveringskost { get; set; }
        public DateTime BestelDatum { get; set; }
        public bool IsGefinaliseerd { get; set; }

    }
}
