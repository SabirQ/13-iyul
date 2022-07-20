using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Utilities;
using Task_MVC.DAL;
using Task_MVC.Models;

namespace Task.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> model = _context.Sliders.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();

            if (slider.Photo is null)
            {
                ModelState.AddModelError("Photo", "You have to choose 1 image at least");
                return View();
            }
            if (!slider.Photo.ImageIsOkay(2))
            {
                ModelState.AddModelError("Photo", "Please choose valid image file");
                return View();
            }

            slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, "assets/images/website-images");

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();
            return View(slider);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            //if (id == 0 || id is null) return NotFound(); 
            Slider existed = await _context.Sliders.FindAsync(id);
            if (existed is null) return NotFound();
            if (!ModelState.IsValid) return View(existed);


            if (slider.Photo == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = filename;
                //existed.Title = slider.Title;
                //existed.Article = slider.Article;
                //existed.ButtonUrl = slider.ButtonUrl;
                //existed.Order = slider.Order;
            }
            else
            {
                if (!slider.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Photo", "Please choose valid image file");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", existed.Image);
                _context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = await slider.Photo.FileCreate(_env.WebRootPath, "assets/images/website-images");
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Slider existed = await _context.Sliders.FindAsync(id);
            if (existed is null) return NotFound();

            if (existed.Image != null)
            {
                FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", existed.Image);
                _context.Sliders.Remove(existed);
            }
           
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
