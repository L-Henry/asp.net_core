using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Configuration.Models;
using Microsoft.Extensions.Configuration;

namespace Configuration.Controllers
{
    public class HelloController : Controller
    {
        private readonly IConfiguration _configuration;

        public HelloController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            string myValue = _configuration["MyKey"];

            ViewData["uitgelezenWaarde"] = myValue;

            return View();
        }

        public IActionResult Detail() {
            string abTest = _configuration["abTestingDetail"];
            ViewData["abTestResult"] = abTest;

            if (abTest.FirstOrDefault() == 'a')
            {            
                return View("aResult");
            }
            else if (abTest.FirstOrDefault() == 'b')
            {             
                return View("bResult");
            }
            else {
                return View();
            }
        }

        public IActionResult Developer() {

            DeveloperDetailViewModel model = new DeveloperDetailViewModel()
            {
                FirstName = _configuration["Developer:FirstName"],
                LastName = _configuration["Developer:LastName"]
            };

            //throw new Exception("");
            return View(model);
        }

        public IActionResult Car() {
            CarDetailViewModel vm = new CarDetailViewModel()
            {
                Brand = _configuration["Car:Brand"],
                FuelType = _configuration["Car:FuelType"]
            };

            return View(vm);
        }







        public IActionResult Privacy()
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
