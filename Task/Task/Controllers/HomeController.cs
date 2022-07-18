using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Task_MVC.DAL;
using Task_MVC.ViewModels;

namespace Task_MVC.Controllers
{
    public class HomeController:Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Plants = _context.Plants.Include(p => p.PlantImages).ToList()
            };
            return View(model);
        }
    }
}
