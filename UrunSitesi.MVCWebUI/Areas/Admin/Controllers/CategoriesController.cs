using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Tools;

namespace UrunSitesi.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View(_dbContext.Categories.ToList());
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Categories.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
            return View(model);
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                        collection.Image = FileHelper.FileLoader(Image);
                    await _dbContext.Categories.AddAsync(collection);
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

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Categories.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
            return View(model);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category collection, IFormFile? Image, bool resmiSil)
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
                    _dbContext.Categories.Update(collection);
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

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id alanı gereklidir!");
            }
            var model = _dbContext.Categories.Find(id);
            if (model == null)
                return NotFound("Kayıt Bulunamadı!");
            return View(model);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category collection)
        {
            try
            {
                FileHelper.FileRemover(collection.Image);
                _dbContext.Categories.Remove(collection);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(collection);
            }
        }
    }
}
