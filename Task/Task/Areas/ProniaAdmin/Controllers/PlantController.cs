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
    public class PlantController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PlantController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Plant> model = _context.Plants
                .Include(p => p.PlantInformation)
                .Include(p => p.PlantCategories).ThenInclude(pc => pc.Category)
                .Include(p => p.PlantTags).ThenInclude(pc => pc.Tag)
                .Include(p => p.PlantColors).ThenInclude(pc => pc.Color)
                .Include(p => p.PlantSizes).ThenInclude(pc => pc.Size)
                .Include(p => p.PlantImages).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Information = _context.PlantInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Plant plant)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Information = _context.PlantInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Colors = _context.Colors.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();
                return View();
            }
            if (plant.MainPhoto == null || plant.HoverPhoto == null || plant.Photos == null)
            {

                ViewBag.Information = _context.PlantInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Colors = _context.Colors.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();
                ModelState.AddModelError(string.Empty, "You must choose 1 main photo and 1 hover photo and 1 another photo");
                return View();
            }

            if (!plant.MainPhoto.ImageIsOkay(2) || !plant.HoverPhoto.ImageIsOkay(2))
            {
                ViewBag.Information = _context.PlantInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Colors = _context.Colors.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();
                ModelState.AddModelError(string.Empty, "Please choose valid image file");
                return View();
            }

            TempData["FileName"] = "";
            foreach (var photo in plant.Photos)
            {
                if (!photo.ImageIsOkay(2))
                {
                    plant.Photos.Remove(photo);
                    TempData["FileName"] += photo.FileName + ",";
                }
                else
                {
                    PlantImage other = new PlantImage
                    {
                        Name = await photo.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                        Primary = false,
                        Alternative = plant.Name,
                        Plant = plant
                    };
                    _context.PlantImages.Add(other);
                }
            }

            PlantImage main = new PlantImage
            {
                Name = await plant.MainPhoto.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                Primary = true,
                Alternative = plant.Name,
                Plant = plant
            };
            PlantImage hover = new PlantImage
            {
                Name = await plant.HoverPhoto.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                Primary = null,
                Alternative = plant.Name,
                Plant = plant
            };
            foreach (var item in plant.CategoryIds)
            {
                PlantCategory pCategory = new PlantCategory { 

                CategoryId=item,
                Category=_context.Categories.Find(item),
                PlantId=plant.Id,
                Plant=plant
                
                };
                _context.PlantCategories.Add(pCategory);
            }
            foreach (var item in plant.TagsIds)
            {
                PlantTag pTag = new PlantTag
                {

                    TagId = item,
                    Tag = _context.Tags.Find(item),
                    PlantId = plant.Id,
                    Plant = plant

                };
                _context.PlantTags.Add(pTag);
            }
            foreach (var item in plant.ColorsIds)
            {
                PlantColor pColor = new PlantColor
                {

                    ColorId = item,
                    Color = _context.Colors.Find(item),
                    PlantId = plant.Id,
                    Plant = plant

                };
                _context.PlantColors.Add(pColor);
            }
            foreach (var item in plant.SizesIds)
            {
                PlantSize pSize = new PlantSize
                {

                    SizeId = item,
                    Size = _context.Sizes.Find(item),
                    PlantId = plant.Id,
                    Plant = plant

                };
                _context.PlantSizes.Add(pSize);
            }

            _context.PlantImages.Add(main);
            _context.PlantImages.Add(hover);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Information = _context.PlantInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            if (id == 0 || id is null) return NotFound();
            Plant plant = await _context.Plants.Include(p => p.PlantCategories).ThenInclude(pc => pc.Category).Include(p=>p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            if (plant is null) return NotFound();
            return View(plant);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int? id, Plant plant)
        {
            ViewBag.Information = _context.PlantInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Colors = _context.Colors.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();

            Plant existed = await _context.Plants.Include(p => p.PlantCategories).ThenInclude(pc => pc.Category).Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            if (existed is null) return NotFound();
            if (!ModelState.IsValid) {
                ViewBag.Information = _context.PlantInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Colors = _context.Colors.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();
                return View(existed); }
                
            if (plant.MainPhoto != null)
            {
                if (!plant.MainPhoto.ImageIsOkay(2))
                {
                    ViewBag.Information = _context.PlantInformations.ToList();
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.Tags = _context.Tags.ToList();
                    ViewBag.Colors = _context.Colors.ToList();
                    ViewBag.Sizes = _context.Sizes.ToList();
                    ModelState.AddModelError("MainPhoto", "Please choose valid image file");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", existed.PlantImages.FirstOrDefault(p => p.Primary == true).Name);
                existed.PlantImages.FirstOrDefault(p => p.Primary == true).Name = await plant.MainPhoto.FileCreate(_env.WebRootPath, "assets/images/website-images");
            }
            if (plant.HoverPhoto != null)
            {
                if (!plant.HoverPhoto.ImageIsOkay(2))
                {
                    ViewBag.Information = _context.PlantInformations.ToList();
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.Tags = _context.Tags.ToList();
                    ViewBag.Colors = _context.Colors.ToList();
                    ViewBag.Sizes = _context.Sizes.ToList();
                    ModelState.AddModelError("MainPhoto", "Please choose valid image file");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", existed.PlantImages.FirstOrDefault(p => p.Primary == null).Name);

                existed.PlantImages.FirstOrDefault(p => p.Primary == null).Name = await plant.HoverPhoto.FileCreate(_env.WebRootPath, "assets/images/website-images");
            }
            if (plant.PlantImageIds == null)
            {
                if (plant.Photos == null)
                {
                    ViewBag.Information = _context.PlantInformations.ToList();
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.Tags = _context.Tags.ToList();
                    ViewBag.Colors = _context.Colors.ToList();
                    ViewBag.Sizes = _context.Sizes.ToList();
                    ModelState.AddModelError("Photos", "You have to choose or keep at least 1 Image");
                    return View(existed);
                } 
                foreach (var item in existed.PlantImages.Where(p => p.Primary == false))
                {
                    FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", item.Name);
                    _context.PlantImages.Remove(item);
                }
                foreach (var item in plant.Photos)
                {
                    PlantImage other = new PlantImage
                    {
                        Name = await item.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                        Primary = false,
                        Alternative = item.FileName,
                        Plant = existed
                    };
                    _context.PlantImages.Add(other);
                }
            }
            else
            {
               
                foreach (var item in existed.PlantImages.Where(p=>p.Primary==false))
                {
                    if (!plant.PlantImageIds.Exists(p=>p==item.Id))
                    {
                        FileValidator.FileDelete(_env.WebRootPath, "assets/images/website-images", item.Name);
                        _context.PlantImages.Remove(item);
                        
                    }
                }
                 
                  
                if (plant.Photos != null)
                {
                    foreach (var photo in plant.Photos)
                    {
                        if (!photo.ImageIsOkay(2))
                        {
                            plant.Photos.Remove(photo);
                            TempData["FileName"] += photo.FileName + ",";
                        }
                        else
                        {
                           
                                PlantImage other = new PlantImage
                                {
                                    Name = await photo.FileCreate(_env.WebRootPath, "assets/images/website-images"),
                                    Primary = false,
                                    Alternative = photo.FileName,
                                    Plant = existed
                                };
                                _context.PlantImages.Add(other);

                        }
                    }
                }
            }
            if (plant.CategoryIds==null)
            {
                ViewBag.Information = _context.PlantInformations.ToList();
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                ViewBag.Colors = _context.Colors.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();
                ModelState.AddModelError("CategoryIds", "You have to choose or keep at least 1 Category");
                return View(existed);
                
            }
            else
            {
                foreach (var item in existed.PlantCategories)
                {
                    if (!plant.CategoryIds.Exists(c => c == item.Id))
                    {
                        _context.PlantCategories.Remove(item);
                    }
                }
                foreach (int newId in plant.CategoryIds)
                {
                    if (!existed.PlantCategories.Exists(c => c.Id == newId))
                    {
                        PlantCategory pCategory = new PlantCategory
                        {

                            CategoryId = newId,
                            Category = _context.Categories.Find(newId),
                            Plant = existed

                        };
                        _context.PlantCategories.Add(pCategory);
                    }
                }
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
