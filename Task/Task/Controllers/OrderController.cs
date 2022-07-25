using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task.Models;
using Task_MVC.DAL;

namespace Task.Controllers
{
    [Authorize(Roles = "Member")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Checkout()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> items = await _context.BasketItems.Include(b => b.AppUser).Include(b => b.Plant).Where(b => b.AppUserId == user.Id).ToListAsync();
            ViewBag.BasketItems = items;
            decimal TotalPrice=0;
            foreach (var item in items)
            {
                TotalPrice+=item.Price*item.Quantity;
            }
            ViewBag.TotalPrice = TotalPrice;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            List<BasketItem> items = await _context.BasketItems.Include(b => b.AppUser).Include(b => b.Plant).Where(b => b.AppUserId == user.Id).ToListAsync();

            ViewBag.BasketItems = items;
            decimal TotalPrice = 0;
            foreach (var item in items)
            {
                TotalPrice += item.Price * item.Quantity;
            }
            ViewBag.TotalPrice = TotalPrice;
            if (!ModelState.IsValid) return View();
            if (items.Count==0)
            {
                ModelState.AddModelError("Address", "Your bag is empty");
                return View();
            }
            order.BasketItems = items;
            order.AppUser = user;
            order.Date = DateTime.Now;
            order.Status = null;
            order.TotalPrice = TotalPrice;
            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
