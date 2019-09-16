using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class OrderItemListViewModel
    {
        public string  UserName { get; set; }
        public string Gerecht { get; set; }

        public string Restaurant { get; set; }
        public decimal OrderPrijs { get; set; }

    }
}
