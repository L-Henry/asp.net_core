using FoodShare.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Data
{
    public class FoodContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Gerechten> Gerechten { get; set; }
        public DbSet<RestaurantType> RestaurantTypes { get; set; }

        public FoodContext(DbContextOptions<FoodContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasKey(r => new { r.TypeId });
            modelBuilder.Entity<OrderItem>().HasKey(o => new { o.GerechtId, o.OrderId, o.UserId });
            modelBuilder.Entity<Gerechten>().HasKey(g => new { g.RestaurantId });
            base.OnModelCreating(modelBuilder);
        }







    }
}
