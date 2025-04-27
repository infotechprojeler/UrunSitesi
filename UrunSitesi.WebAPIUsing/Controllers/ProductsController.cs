using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;

namespace UrunSitesi.WebAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Products/";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(string q = "")
        {
            if (!string.IsNullOrWhiteSpace(q))
                _apiAdres += "Search/" + q;
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
            return View(products);
        }
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
                return BadRequest("Geçersiz İstek!"); // id yoksa

            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);

            var model = products.Where(p => p.IsActive && p.Id == id).FirstOrDefault();

            if (model == null)
                return NotFound("Ürün Bulunamadı!"); // id var ama eşleşen kayıt yoksa
            
            ViewBag.Products = products.Where(p => p.CategoryId == model.CategoryId && p.Id != id);
            return View(model); // her şey yolundaysa ekrana modeli yolla
        }
    }
}
