using FoodShare.Data;
using FoodShare.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodShare.Controllers
{
    public class GerechtenController : Controller
    {
        private readonly FoodContext _context;

        public GerechtenController(FoodContext context)
        {
            _context = context;
        }


        public IActionResult GerechtCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GerechtCreate(GerechtCreateViewModel model)
        {
            return View();
        }

        public IActionResult GerechtEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerechtEdit(GerechtEditViewModel model)
        {
            return View();
        }

        public IActionResult GerechtList()
        {
            return View();
        }

    }
}
