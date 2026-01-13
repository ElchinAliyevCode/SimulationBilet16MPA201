using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimulationBilet16MPA201.Models;

namespace SimulationBilet16MPA201.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
