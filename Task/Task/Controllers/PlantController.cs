using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Task.DAL;
using Task.Models;

namespace Task.Controllers
{
    public class PlantController:Controller
    {

        private readonly AppDbContext _context;

        public PlantController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Plant plant = await _context.Plants.Include(p => p.PlantImages)
                .Include(p => p.PlantInformation).Include(p => p.PlantCategories).ThenInclude(p => p.Category).Include(p=>p.PlantTags).ThenInclude(p=>p.Tag)
                .Include(p => p.PlantColors).ThenInclude(p => p.Color).Include(p => p.PlantSizes).ThenInclude(p => p.Size).FirstOrDefaultAsync(p => p.Id == id);
            if (plant is null) return NotFound();

            return View(plant);
        }
    }
}
