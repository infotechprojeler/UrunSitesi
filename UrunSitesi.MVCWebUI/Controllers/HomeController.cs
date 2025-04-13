using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Models;
using UrunSitesi.MVCWebUI.Tools;

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

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    //await MailHelper.SendMailAsync(contact);
                    TempData["Message"] = @$"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
  <strong>Ýþlem Baþarýlý!</strong> Mesajýnýz Ýletilmiþtir.
  <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
</div>";
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata Oluþtu!");
                }
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
