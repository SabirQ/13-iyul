using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_MVC.ViewModels;
using Task_MVC.DAL;
using Task_MVC.Models;
using Task.Models;
using Microsoft.AspNetCore.Identity;

namespace Task_MVC.Controllers
{
    public class PlantController:Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PlantController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            List<Plant> plants = new List<Plant>();
            List<Plant> plantsRange = new List<Plant>();
            foreach (var item in plant.PlantCategories)
            {
                plants = _context.Plants.Where(x=>x.Id!=id).Where(x => x.PlantCategories.Any(z => z.CategoryId == item.CategoryId)).Include(x => x.PlantImages).ToList();

                plantsRange.AddRange(plants);
            }
            PlantVM plantVM = new PlantVM
            {
                Plant=plant,
                Plants= plantsRange.Distinct().ToList(),
            };
          
            if (plant is null) return NotFound();

            return View(plantVM);
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Plant plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            if (User.Identity.IsAuthenticated && User.IsInRole("Member"))
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (user == null) return NotFound();
                BasketItem existed = await _context.BasketItems.FirstOrDefaultAsync(b => b.AppUserId == user.Id && b.PlantId == plant.Id);
                if (existed == null)
                {
                    existed = new BasketItem
                    {
                        Plant = plant,
                        AppUser = user,
                        Quantity = 1,
                        Price = plant.Price
                    };
                    _context.BasketItems.Add(existed);
                }
                else
                {
                    existed.Quantity++;
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                string basketStr = HttpContext.Request.Cookies["Basket"];

                BasketVM basket;

                if (string.IsNullOrEmpty(basketStr))
                {
                    basket = new BasketVM();
                    BasketCookieItemVM cookieItem = new BasketCookieItemVM
                    {
                        Id = plant.Id,
                        Quantity = 1
                    };
                    basket.BasketCookieItemVMs = new List<BasketCookieItemVM>();
                    basket.BasketCookieItemVMs.Add(cookieItem);
                    basket.TotalPrice = plant.Price;

                }
                else
                {
                    basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                    BasketCookieItemVM existed = basket.BasketCookieItemVMs.Find(p => p.Id == id);
                    if (existed == null)
                    {
                        BasketCookieItemVM cookieItem = new BasketCookieItemVM
                        {
                            Id = plant.Id,
                            Quantity = 1
                        };
                        basket.BasketCookieItemVMs.Add(cookieItem);
                        basket.TotalPrice += plant.Price;
                    }
                    else
                    {
                        basket.TotalPrice += plant.Price;
                        existed.Quantity++;
                    }
                }
                basketStr = JsonConvert.SerializeObject(basket);

                HttpContext.Response.Cookies.Append("Basket", basketStr);
            }

            return RedirectToAction("Index", "Home");
        }



        public IActionResult ShowBasket()
        {
            if (HttpContext.Request.Cookies["Basket"] == null) return NotFound();
            BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(HttpContext.Request.Cookies["Basket"]);
            return Json(basket);
        }
        public async Task<IActionResult> RemoveBasket(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Plant plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();
            string basketStr = HttpContext.Request.Cookies["Basket"];

            BasketVM basket;

            if (string.IsNullOrEmpty(basketStr))
            {
               return NotFound();
            }
            else
            {
                basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                BasketCookieItemVM existed = basket.BasketCookieItemVMs.Find(p => p.Id == id);
                if (existed == null)
                {
                    return NotFound();
                }
                else
                {
                    basket.TotalPrice -= existed.Quantity*plant.Price;
                    basket.BasketCookieItemVMs.Remove(existed);
                }
            }
            basketStr = JsonConvert.SerializeObject(basket);

            HttpContext.Response.Cookies.Append("Basket", basketStr);

            return RedirectToAction("Index", "Home");
        }

    }
}
