using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulationBilet16MPA201.Contexts;
using SimulationBilet16MPA201.Helpers;
using SimulationBilet16MPA201.Models;
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

    [HttpPost]
    public async Task<IActionResult> Create(TrainerCreateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("", "Category not found");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("", "Max 2mb");
            return View(vm);
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("", "Image must be in correct form");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.UploadFileAsync(_folderPath);

        Trainer trainer = new Trainer()
        {
            FirstName=vm.FirstName,
            LastName=vm.LastName,
            Description=vm.Description,
            CategoryId=vm.CategoryId,
            ImagePath=uniqueFileName,
        };

        await _context.Trainers.AddAsync(trainer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(int id)
    {
        var trainer = await _context.Trainers.FindAsync(id);

        if (trainer == null)
        {
            return NotFound();
        }

        _context.Trainers.Remove(trainer);
        await _context.SaveChangesAsync();

        var deletedFolder = Path.Combine(_folderPath, trainer.ImagePath);

        FileHelper.DeleteFile(deletedFolder);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int id)
    {
        var trainer = await _context.Trainers.FindAsync(id);

        if(trainer == null)
        {
            return NotFound();
        }

        TrainerUpdateVM vm = new TrainerUpdateVM()
        {
            Id=trainer.Id,
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            Description = trainer.Description,
            CategoryId = trainer.CategoryId,
        };

        await SendCategoriesWithViewBag();


        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TrainerUpdateVM vm)
    {
        await SendCategoriesWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var trainer =await _context.Trainers.FirstOrDefaultAsync(x => x.Id == vm.Id);
        if (trainer == null)
        {
            return NotFound();
        }

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            ModelState.AddModelError("", "Category not found");
            return View(vm);
        }

        if (!vm.Image?.CheckSize(2)??false)
        {
            ModelState.AddModelError("", "Max 2mb");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image")??false)
        {
            ModelState.AddModelError("", "Image must be in correct form");
            return View(vm);
        }

        trainer.FirstName=vm.FirstName;
        trainer.LastName=vm.LastName;
        trainer.Description=vm.Description;
        trainer.CategoryId=vm.CategoryId;

        if(vm.Image is { })
        {
            var fileName = await vm.Image.UploadFileAsync(_folderPath);
            string deletedPath = Path.Combine(_folderPath, trainer.ImagePath);

            FileHelper.DeleteFile(deletedPath);

            trainer.ImagePath = fileName;
        }

        _context.Trainers.Update(trainer);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));

    }



    private async Task SendCategoriesWithViewBag()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;
    }
}
