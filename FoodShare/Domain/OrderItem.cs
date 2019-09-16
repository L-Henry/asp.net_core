using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Domain
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int GerechtId { get; set; }
        public Gerechten Gerecht { get; set; }
        public decimal OrderPrijs { get; set; }
        public int Aantal { get; set; } = 1;

    }
}
