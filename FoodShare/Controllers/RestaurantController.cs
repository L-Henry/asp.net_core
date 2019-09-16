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
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IRestaurantTypeService _restaurantTypeService;


        public RestaurantController(IRestaurantService restaurantService, IRestaurantTypeService restaurantTypeService)
        {
            _restaurantService = restaurantService;
            _restaurantTypeService = restaurantTypeService;
        }


        public IActionResult RestaurantCreate()
        {
            List<SelectListItem> typesList = new List<SelectListItem>();
            IEnumerable<RestaurantType> types = _restaurantTypeService.Get();
            foreach (var type in types)
            {
                typesList.Add(new SelectListItem { Value = type.Id.ToString(), Text = type.Naam});
            }
            RestaurantCreateViewModel vm = new RestaurantCreateViewModel
            {
                TypeList = typesList
            };
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult RestaurantCreate(RestaurantCreateViewModel model)
        {
            Restaurant restaurant = new Restaurant
            {
                Naam = model.Naam,
                TypeId = _restaurantTypeService.Get(model.TypeId).Id,
                NogOpen = true           
            };
            _restaurantService.Insert(restaurant);

            return RedirectToAction("RestaurantDetail", new { id = restaurant.Id });
        }

        public IActionResult RestaurantDetail(int id)
        {
            Restaurant restoFromDb = _restaurantService.Get(id);
            RestaurantDetailViewModel vm = new RestaurantDetailViewModel {
                Naam = restoFromDb.Naam
            };


            return View(vm);
        }



        public IActionResult RestaurantEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RestaurantEdit(RestaurantEditViewModel model)
        {
            return View();
        }
        public IActionResult RestaurantList()
        {
            return View();
        }



    }
}
