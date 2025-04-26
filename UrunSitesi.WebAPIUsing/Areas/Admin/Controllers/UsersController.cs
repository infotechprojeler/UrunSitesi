using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        string _apiAdres = "https://localhost:7279/api/Users/";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);
            return View(model);
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiAdres + id);
            return View(model);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(User collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
            return View(collection);
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiAdres + id);
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, User collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
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
            return View(collection);
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<User>(_apiAdres + id);
            return View(model);
        }

        // POST: UsersController/Delete/5
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
