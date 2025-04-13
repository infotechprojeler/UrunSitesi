using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Data;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ProductsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string q = "")
        {
            return View(_dbContext.Products.Where(p => p.Name.Contains(q)));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
                return BadRequest("Geçersiz İstek!"); // id yoksa
            var model = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (model == null)
                return NotFound("Ürün Bulunamadı!"); // id var ama eşleşen kayıt yoksa
            ViewBag.Products = _dbContext.Products.Where(p => p.CategoryId == model.CategoryId);
            return View(model); // her şey yolundaysa ekrana modeli yolla
        }
    }
}
