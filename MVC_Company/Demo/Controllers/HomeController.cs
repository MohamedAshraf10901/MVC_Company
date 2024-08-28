using MVC_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Demo.PL.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace MVC_Demo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scoped1;
        private readonly IScopedService scoped2;
        private readonly ITransientService transient1;
        private readonly ITransientService transient2;
        private readonly ISingeltonService singelton1;
        private readonly ISingeltonService singelton2;

        public HomeController(ILogger<HomeController> logger    // Ask Clr Create Object from Class implement ILogger
                                ,IScopedService scoped1
                                ,IScopedService scoped2
                                ,ITransientService transient1
                                ,ITransientService transient2
                                ,ISingeltonService singelton1
                                ,ISingeltonService singelton2)  
        {
            _logger = logger;
            this.scoped1 = scoped1;
            this.scoped2 = scoped2;
            this.transient1 = transient1;
            this.transient2 = transient2;
            this.singelton1 = singelton1;
            this.singelton2 = singelton2;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Home/TestLifeTime
        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"scoped1 :: {scoped1.GetGuid()}\n");
            builder.Append($"scoped2 :: {scoped2.GetGuid()}\n\n");
            builder.Append($"transient1 :: {transient1.GetGuid()}\n");
            builder.Append($"transient2 :: {transient2.GetGuid()}\n\n");
            builder.Append($"singelton1 :: {singelton1.GetGuid()}\n");
            builder.Append($"singelton2 :: {singelton2.GetGuid()}\n\n");


            return builder.ToString();
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
