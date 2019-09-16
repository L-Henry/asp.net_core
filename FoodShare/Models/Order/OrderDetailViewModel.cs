using FoodShare.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Models
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }

        public string Restaurant { get; set; }

        public decimal ToaalPrijs { get; set; }

        public List<OrderItemListViewModel> Orders { get; set; }

        public decimal Leveringskost { get; set; }
        public DateTime BestelDatum { get; set; }
        public bool IsGefinaliseerd { get; set; }
    }
}
