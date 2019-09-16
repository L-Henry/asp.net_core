using FoodShare.Data;
using FoodShare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly FoodContext _context;

        public RestaurantService(FoodContext context)
        {
            _context = context;
        }


        public IEnumerable<Restaurant> Get()
        {
            return _context.Restaurants;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.SingleOrDefault(t => t.Id == id);
        }

        public void Insert(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void Update(Restaurant restaurant, int id)
        {
            Restaurant restaurantToUpdate = Get(id);
            if (restaurantToUpdate != null)
            {
                restaurantToUpdate.Naam = restaurant.Naam;
                _context.SaveChanges();
            }
        }

    }
}
