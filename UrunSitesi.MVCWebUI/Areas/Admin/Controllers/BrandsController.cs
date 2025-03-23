using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Tools;

namespace UrunSitesi.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public BrandsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: BrandsController
        public ActionResult Index()
        {
            return View(_dbContext.Brands);
        }

        // GET: BrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand collection, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo is not null)
                    {
                        collection.Logo = FileHelper.FileLoader(Logo);
                    }
                    _dbContext.Brands.Add(collection);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }            
            return View(collection);
        }

        // GET: BrandsController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _dbContext.Brands.Find(id);
            return View(model);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brand collection, IFormFile? Logo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Logo is not null)
                    {                        
                        collection.Logo = FileHelper.FileLoader(Logo);
                    }
                    _dbContext.Brands.Update(collection);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            return View(collection);
        }

        // GET: BrandsController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _dbContext.Brands.Find(id);
            return View(model);
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Brand collection)
        {
            try
            {
                FileHelper.FileRemover(collection.Logo);
                _dbContext.Brands.Remove(collection);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
