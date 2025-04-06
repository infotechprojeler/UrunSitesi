using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;
using UrunSitesi.MVCWebUI.Tools;
using static System.Net.Mime.MediaTypeNames;

namespace UrunSitesi.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ProductsController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            var model = _dbContext.Products // veritabanından ürün listesini çek
                .Include(c => c.Category)  // ürünlere kategorilerini dahil et
                .Include(c => c.Brand);   // ürünlere markasını da dahil et
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            var model = _dbContext.Products // ürünlerden
                .Where(c => c.Id == id) // id si route dan gelenle eşleşeni seç
                .Include(c => c.Category) // kategorisini dahil et
                .Include(c => c.Brand) // markasını dahil et
                .FirstOrDefault(); // p => p.Id == id
            return View(model);
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(_dbContext.Brands, "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product collection, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                        collection.Image = FileHelper.FileLoader(Image);
                    _dbContext.Products.Add(collection);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(_dbContext.Brands, "Id", "Name");
            return View(collection);
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(_dbContext.Brands, "Id", "Name");
            if (id == null)
                return BadRequest("Id Gereklidir!");
            var model = _dbContext.Products.Find(id);
            if (model == null)
                return NotFound("Id ile eşleşen kayıt bulunamadı!");
            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product collection, IFormFile? Image, bool resmiSil)
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
                    _dbContext.Products.Update(collection);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }
            ViewBag.Kategoriler = new SelectList(_dbContext.Categories, "Id", "Name");
            ViewBag.BrandId = new SelectList(_dbContext.Brands, "Id", "Name");
            return View(collection);
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _dbContext.Products // ürünlerden
                .Where(c => c.Id == id) // id si route dan gelenle eşleşeni seç
                .Include(c => c.Category) // kategorisini dahil et
                .Include(c => c.Brand) // markasını dahil et
                .FirstOrDefault(); // p => p.Id == id
            return View(model); // modeli ekrana yolla
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product collection)
        {
            try
            {
                _dbContext.Products.Remove(collection);
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
