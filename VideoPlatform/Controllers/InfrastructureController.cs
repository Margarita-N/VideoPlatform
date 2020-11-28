using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace VideoPlatform.Controllers
{
    public class InfrastructureController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }

    }
}
