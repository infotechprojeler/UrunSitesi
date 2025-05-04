using Microsoft.AspNetCore.Mvc;

namespace UrunSitesi.WebAPIUsing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JsCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
