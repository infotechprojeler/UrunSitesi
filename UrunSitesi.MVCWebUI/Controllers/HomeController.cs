using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Models;
using UrunSitesi.MVCWebUI.Tools;
using UrunSitesi.Service;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;
        private readonly IService<Product> _serviceProduct; // IService Product sýnýfý için çalýþsýn
        private readonly IService<Slider> _serviceSlider; // IService Slider sýnýfý için çalýþsýn
        private readonly IService<Contact> _serviceContact;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context, IService<Product> serviceProduct, IService<Slider> serviceSlider, IService<Contact> serviceContact)
        {
            _logger = logger;
            _context = context;
            _serviceProduct = serviceProduct;
            _serviceSlider = serviceSlider;
            _serviceContact = serviceContact;
        }

        public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                //Products = _context.Products.Where(x => x.IsActive && x.IsHome),
                //Sliders = _context.Sliders
                Products = _serviceProduct.GetAll(x => x.IsActive && x.IsHome), // ürün listesini özel servisimizden çek
                Sliders = _serviceSlider.GetAll() // slider listesini özel servisimizden çek
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
        public async Task<IActionResult> ContactAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //await _context.Contacts.AddAsync(contact); // _context ile ekleme
                    //await _context.SaveChangesAsync(); // _context ile kaydetme
                    await _serviceContact.AddAsync(contact); // servis ile ekleme
                    await _serviceContact.SaveAsync(); // servis ile kaydetme
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
