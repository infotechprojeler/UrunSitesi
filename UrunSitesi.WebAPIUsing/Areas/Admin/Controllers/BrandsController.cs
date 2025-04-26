using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrunSitesi.Core.Entities;
using UrunSitesi.WebAPIUsing.Tools;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Brands/";
        HttpClient httpClient = new HttpClient(); // api ye istek atmamızı sağlayacak sınıfımız.

        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            var model = await httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres);
            return View(model);
        }

        // GET: BrandsController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var model = await httpClient.GetFromJsonAsync<Brand>(_apiAdres + id);
            return View(model);
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand collection, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo is not null)
                    {
                        collection.Logo = FileHelper.FileLoader(Logo);
                    }
                    var response = await httpClient.PostAsJsonAsync(_apiAdres, collection);
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

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await httpClient.GetFromJsonAsync<Brand>(_apiAdres + id);
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Brand collection, IFormFile? Logo, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo is not null)
                    {
                        collection.Logo = FileHelper.FileLoader(Logo);
                    }
                    if (resmiSil == true)
                    {
                        collection.Logo = string.Empty;
                        FileHelper.FileRemover(collection.Logo);
                    }
                    var response = await httpClient.PutAsJsonAsync(_apiAdres, collection);
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ModelState.AddModelError("", "Kayıt Başarısız!");
            return View(collection);
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await httpClient.GetFromJsonAsync<Brand>(_apiAdres + id);
            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
