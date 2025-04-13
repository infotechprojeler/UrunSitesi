using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Models;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Products = _context.Products.Where(x => x.IsActive && x.IsHome),
                Sliders = _context.Sliders
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
