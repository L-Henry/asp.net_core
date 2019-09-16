using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Domain
{
    public class Order
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        public decimal ToaalPrijs { get; set; }

        public ICollection<OrderItem> Orders { get; set; }

        public decimal Leveringskost { get; set; }
        public DateTime BestelDatum { get; set; }
        public bool IsGefinaliseerd { get; set; }

    }
}
