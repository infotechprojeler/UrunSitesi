using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.Service;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _dbContext;
        private readonly IService<Product> _serviceProduct;

        public ProductsController(DatabaseContext dbContext, IService<Product> serviceProduct)
        {
            _dbContext = dbContext;
            _serviceProduct = serviceProduct;
        }
        public IActionResult Index(string q = "")
        {
            // return View(_dbContext.Products.Where(p => p.Name.Contains(q)));
            return View(_serviceProduct.GetAll(p => p.Name.Contains(q)));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Geçersiz İstek!"); // id yoksa
            // var model = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            var model = _serviceProduct.Get(p => p.Id == id); // kendi yazdığımız servisdeki get metodu lambda expression ile filtreye göre çalışıyor
            if (model == null)
                return NotFound("Ürün Bulunamadı!"); // id var ama eşleşen kayıt yoksa
            ViewBag.Products = _dbContext.Products.Where(p => p.CategoryId == model.CategoryId);
            return View(model); // her şey yolundaysa ekrana modeli yolla
        }
    }
}
