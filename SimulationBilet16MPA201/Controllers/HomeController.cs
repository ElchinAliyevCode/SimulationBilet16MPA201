using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationBilet16MPA201.Contexts;
using SimulationBilet16MPA201.Models;
using SimulationBilet16MPA201.ViewModels.TrainerViewModels;
using System.Diagnostics;

namespace SimulationBilet16MPA201.Controllers
{
    public class HomeController : Controller
    {
        private readonly SimulationDbContext _context;

        public HomeController(SimulationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var trainers = await _context.Trainers.Select(x => new TrainerGetVM()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Description = x.Description,
                CategoryName = x.Category.Name,
                ImagePath = x.ImagePath
            }).ToListAsync();
            return View(trainers);
        }
    }
}
