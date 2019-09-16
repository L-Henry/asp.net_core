using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodShare.Models;
using FoodShare.Data;
using FoodShare.Domain;

namespace FoodShare.Controllers
{
    public class OrderController : Controller
    {

        private readonly FoodContext _context;

        public OrderController(FoodContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        public IActionResult OrderCreate()
        {
            return View();
        }
        public IActionResult OrderDetail()
        {
            return View();
        }
        public IActionResult OrderEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OrderEdit(OrderEditViewModel model)
        {
            return View();
        }
        public IActionResult OrderList()
        {
            return View();
        }

 


 









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
