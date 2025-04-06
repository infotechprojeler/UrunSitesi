using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Tools;

namespace UrunSitesi.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public SlidersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: SlidersController
        public ActionResult Index()
        {
            return View(_dbContext.Sliders);
        }

        // GET: SlidersController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Sliders.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
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
                        collection.Image = FileHelper.FileLoader(Image);
                    await _dbContext.Sliders.AddAsync(collection);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: SlidersController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Sliders.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
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
                        collection.Image = FileHelper.FileLoader(Image);
                    if (resmiSil == true)
                    {
                        collection.Image = string.Empty;
                        FileHelper.FileRemover(collection.Image);
                    }
                    _dbContext.Sliders.Update(collection);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: SlidersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Sliders.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
            return View(model);
        }

        // POST: SlidersController/Delete/5
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
