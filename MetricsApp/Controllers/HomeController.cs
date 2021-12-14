using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("data1")]
        public string GetData1()
        {
            return $"data 1";
        }

        [Route("data2")]
        public string GetData2()
        {
            return $"data 2";
        }
    }
}
