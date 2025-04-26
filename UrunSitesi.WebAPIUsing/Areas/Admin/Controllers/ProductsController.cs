using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunSitesi.Core.Entities;
using UrunSitesi.WebAPIUsing.Tools;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Products/";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: ProductsController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres);
            return View(model);
        }

        // GET: ProductsController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + id);
            return View(model);
        }

        async Task LoadAsync()
        {
            var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>("https://localhost:7279/api/Categories/");
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Name");
            var markalar = await _httpClient.GetFromJsonAsync<List<Brand>>("https://localhost:7279/api/Brands/");
            ViewBag.BrandId = new SelectList(markalar, "Id", "Name");
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> CreateAsync()
        {
            await LoadAsync();
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        collection.Image = FileHelper.FileLoader(Image);
                    }
                    var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                    if (response.IsSuccessStatusCode) // eğer api den işlem başarılı mesajı dönerse
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            await LoadAsync();
            return View(collection);
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            await LoadAsync();
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + id);
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product collection, IFormFile? Image, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        collection.Image = FileHelper.FileLoader(Image);
                    }
                    if (resmiSil == true)
                    {
                        collection.Image = string.Empty;
                        FileHelper.FileRemover(collection.Image);
                    }
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres + id, collection);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            await LoadAsync();
            return View(collection);
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiAdres + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
