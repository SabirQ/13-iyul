using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_MVC.DAL;
using Task_MVC.Models;

namespace Task.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class ColorController : Controller
    {
        private readonly AppDbContext _context;

        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Color> model = _context.Colors.OrderByDescending(x => x.Id).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Color color)
        {
            if (!ModelState.IsValid) return View();
            Color existed = _context.Colors.FirstOrDefault(c => c.Name.ToLower().Trim() == color.Name.ToLower().Trim());
            if (existed != null)
            {
                ModelState.AddModelError("Name", "You can not duplicate category name");
                return View();
            }
            _context.Colors.Add(color);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Color color = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (color is null) return NotFound();
            return View(color);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, Color newColor)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();

            Color existed = _context.Colors.FirstOrDefault(c => c.Id == id);
            if (existed == null) return NotFound();
            bool duplicate = _context.Colors.Any(c => c.Name == newColor.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "You cannot duplicate name");
                return View();
            }

            _context.Entry(existed).CurrentValues.SetValues(newColor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Color color = await _context.Colors.FindAsync(id);
            if (color is null) return NotFound();

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Color color = await _context.Colors.FindAsync(id);
            if (color is null) return NotFound();
            return View(color);
        }
    }
}
