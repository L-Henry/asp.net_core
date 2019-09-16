using FoodShare.Data;
using FoodShare.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly FoodContext _context;

        public OrderItemController(FoodContext context)
        {
            _context = context;
        }





        public IActionResult OrderItemCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult OrderItemCreate(OrderItemCreateViewModel model)
        {
            return View();
        }

        public IActionResult OrderItemEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderItemEdit(OrderItemEditViewModel model)
        {
            return View();
        }
        public IActionResult OrderItemList()
        {
            return View();
        }




    }
}
