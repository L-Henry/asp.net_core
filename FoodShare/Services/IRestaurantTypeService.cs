using FoodShare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public interface IRestaurantTypeService
    {
        IEnumerable<RestaurantType> Get();
        RestaurantType Get(int id);
        void Insert(RestaurantType type);
        void Update(RestaurantType type, int id);


    }
}
