using FoodShare.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Services
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> Get();
        Restaurant Get(int id);
        void Insert(Restaurant type);
        void Update(Restaurant type, int id);

    }
}
