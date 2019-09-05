using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieWeb.Models;
using MovieWeb.Services;

namespace MovieWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITransientService _transientService;
        private readonly IScopedService _scopedService;
        private readonly ISingletonService _singletonService;

        private readonly ILogger _logger;

        public HomeController(IConfiguration configuration, ITransientService transientService, ISingletonService singletonService, IScopedService scopedService, ILogger<HomeController> logger) {
            _configuration = configuration;
            _transientService = transientService;
            _scopedService = scopedService;
            _singletonService = singletonService;

            _logger = logger;
        }



        public IActionResult DependancyService() {
            DependancyServiceViewModel vm = new DependancyServiceViewModel
            {
                Scoped = _scopedService,
                Transient = _transientService,
                Singelton = _singletonService
            };

            return View(vm);
        }

        public IActionResult Index()
        {

            _logger.LogInformation("Hello to serilog");

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
