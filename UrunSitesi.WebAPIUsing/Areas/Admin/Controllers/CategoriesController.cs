using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunSitesi.Core.Entities;
using UrunSitesi.WebAPIUsing.Tools;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CategoriesController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Categories/";
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }

        public async Task<ActionResult> DetailsAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + id);
            return View(model);
        }

        public async Task<ActionResult> CreateAsync()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            ViewBag.Kategoriler = new SelectList(model, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category collection, IFormFile? Image)
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
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            ViewBag.Kategoriler = new SelectList(model, "Id", "Name");
            return View(collection);
        }

        public async Task<ActionResult> EditAsync(int id)
        {
            var model1 = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            ViewBag.Kategoriler = new SelectList(model1, "Id", "Name");

            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category collection, IFormFile? Image, bool resmiSil)
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
                    if (response.IsSuccessStatusCode) // eğer api den işlem başarılı mesajı dönerse
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            ViewBag.Kategoriler = new SelectList(model, "Id", "Name");
            return View(collection);
        }

        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Category collection)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_apiAdres + id);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View(collection);
        }

    }
}
