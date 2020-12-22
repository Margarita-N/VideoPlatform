using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VideoPlatform.Models;

namespace VideoPlatform.Controllers
{
    public class InfrastructureController : Controller
    {
        public IActionResult CanvasTest()
        {
            return View();
        }

        

    }
}
