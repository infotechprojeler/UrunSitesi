using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Data;

namespace UrunSitesi.MVCWebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public UsersController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            return View(_dbContext.Users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            var model = _dbContext.Users.Find(id);
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
        public ActionResult Create(User collection)
        {
            if (ModelState.IsValid)
            {
                var kullanici = _dbContext.Users.FirstOrDefault(u => u.Email == collection.Email);
                if (kullanici != null)
                {
                    ModelState.AddModelError("Email", "Bu Email Sistemde Kayıtlı!");
                }
                else
                {
                    try
                    {
                        _dbContext.Users.Add(collection);
                        _dbContext.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Hata Oluştu!");
                    }
                }                    
            }
            return View(collection);
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _dbContext.Users.Find(id);
            return View(model);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User collection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Users.Update(collection);
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

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _dbContext.Users.Find(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, User collection)
        {
            try
            {
                _dbContext.Users.Remove(collection);
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
