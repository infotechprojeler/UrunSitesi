using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.WebAPIUsing.Tools;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SlidersController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Sliders/";
        private readonly HttpClient _httpClient;

        public SlidersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: SlidersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres);
            return View(model);
        }

        // GET: SlidersController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slider>(_apiAdres + id);
            return View(model);
        }

        // GET: SlidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slider collection, IFormFile? Image)
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
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                    ModelState.AddModelError("", "Kayıt Başarısız!");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: SlidersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slider>(_apiAdres + id);
            return View(model);
        }

        // POST: SlidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slider collection, IFormFile? Image, bool resmiSil)
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
            return View(collection);
        }

        // GET: SlidersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slider>(_apiAdres + id);
            return View(model);
        }

        // POST: SlidersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
