using FoodShare.Data;
using FoodShare.Domain;
using FoodShare.Models;
using FoodShare.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Controllers
{
    public class RestaurantTypeController : Controller
    {

        private readonly IRestaurantTypeService _service;

        public RestaurantTypeController(IRestaurantTypeService service)
        {
            _service = service;
        }


        public IActionResult RestaurantTypeCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RestaurantTypeCreate(RestaurantTypeCreateViewModel model)
        {
            RestaurantType restaurantType = new RestaurantType
            {
                Naam = model.Naam
            };
            _service.Insert(restaurantType);
            return RedirectToAction("RestaurantTypeList");
        }



        public IActionResult RestaurantTypeEdit(int id)
        {
            RestaurantType typeFromDb = _service.Get(id);
            RestaurantTypeEditViewModel vm = new RestaurantTypeEditViewModel()
            {
                Naam = typeFromDb.Naam
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult RestaurantTypeEdit(RestaurantTypeEditViewModel model)
        {
            RestaurantType type = new RestaurantType
            {
                Naam = model.Naam
            };
            _service.Update(type, model.Id);
            return RedirectToAction("RestaurantTypeList");
        }




        public IActionResult RestaurantTypeList()
        {
            List<RestaurantTypeListViewModel> types = new List<RestaurantTypeListViewModel>();
            IEnumerable<RestaurantType> restaurantTypesFromDb = _service.Get();
            foreach (var type in restaurantTypesFromDb)
            {
                types.Add(new RestaurantTypeListViewModel
                {
                    Id = type.Id,
                    Type = type.Naam
                });
            }

            return View(types);
        }

    }
}
