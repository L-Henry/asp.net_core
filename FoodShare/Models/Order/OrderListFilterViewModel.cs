using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class OrderListFilterViewModel
    {
        public IEnumerable<OrderListViewModel> Orders { get; set; }
        public List<SelectListItem> UserName { get; set; }
        public List<SelectListItem> Restaurant { get; set; }
        public List<SelectListItem> IsGefinaliseerd { get; set; }
        public decimal TotaalPrijs { get; set; }


    }
}
