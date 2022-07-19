using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_MVC.DAL;
using Task_MVC.Models;

namespace Task.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;

        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Size> model = _context.Sizes.OrderByDescending(x => x.Id).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid) return View();
            Size existed = _context.Sizes.FirstOrDefault(c => c.Name.ToLower().Trim() == size.Name.ToLower().Trim());
            if (existed != null)
            {
                ModelState.AddModelError("Name", "You cannot duplicate name");
                return View();
            }
            _context.Sizes.Add(size);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Size size = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (size is null) return NotFound();
            return View(size);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int? id, Size newSize)
        {
            if (id is null || id == 0) return NotFound();
            if (!ModelState.IsValid) return View();

            Size existed = _context.Sizes.FirstOrDefault(c => c.Id == id);
            if (existed == null) return NotFound();
            bool duplicate = _context.Sizes.Any(c => c.Name == newSize.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "You cannot duplicate name");
                return View();
            }

            _context.Entry(existed).CurrentValues.SetValues(newSize);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Size size = await _context.Sizes.FindAsync(id);
            if (size is null) return NotFound();

            _context.Sizes.Remove(size);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null || id == 0) return NotFound();
            Size size = await _context.Sizes.FindAsync(id);
            if (size is null) return NotFound();
            return View(size);
        }
    }
}
