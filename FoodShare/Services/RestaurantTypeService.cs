using FoodShare.Data;
using FoodShare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public class RestaurantTypeService : IRestaurantTypeService
    {


        private readonly FoodContext _context;

        public RestaurantTypeService(FoodContext context)
        {
            _context = context;
        }


        public IEnumerable<RestaurantType> Get()
        {
            return _context.RestaurantTypes;
        }

        public RestaurantType Get(int id)
        {
            return _context.RestaurantTypes.SingleOrDefault(t => t.Id == id);
        }

        public void Insert(RestaurantType type)
        {
            _context.RestaurantTypes.Add(type);
            _context.SaveChanges();
        }

        public void Update(RestaurantType type, int id)
        {
            RestaurantType typeToUpdate = Get(id);
            if (typeToUpdate != null)
            {
                typeToUpdate.Naam = type.Naam;
                _context.SaveChanges();
            }
        }
    }
}
