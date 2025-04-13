using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrunSitesi.Data;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public CategoriesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
                return BadRequest("Id Bilgisi Gereklidir!");
            var model = _dbContext.Categories.Where(c => c.Id == id).Include(p => p.Products).FirstOrDefault();
            if (model == null)
                return NotFound();
            return View(model);
        }
    }
}
