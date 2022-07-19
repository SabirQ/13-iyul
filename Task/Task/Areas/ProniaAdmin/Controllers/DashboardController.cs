using Microsoft.AspNetCore.Mvc;

namespace Task.Areas.ProniaAdmin.Controllers
{
    [Area("ProniaAdmin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
