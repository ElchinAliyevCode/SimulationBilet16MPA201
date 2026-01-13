using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationBilet16MPA201.Contexts;
using SimulationBilet16MPA201.ViewModels.TrainerViewModels;

namespace SimulationBilet16MPA201.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles ="Admin")]
public class TrainerController : Controller
{
    private readonly SimulationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public TrainerController(SimulationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "images");
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

    public async Task<IActionResult> Create()
    {
        await SendCategoriesWithViewBag();
        return View();
    }


    

    private async Task SendCategoriesWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;
    }
}
