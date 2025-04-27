using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;

namespace UrunSitesi.WebAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/";
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
                return BadRequest("Id Bilgisi Gereklidir!");
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "Categories/" + id);
            if (model == null)
                return NotFound();
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            if (products != null)
                model.Products = products.Where(p => p.IsActive && p.CategoryId == id.Value).ToList();
            return View(model);
        }
    }
}
