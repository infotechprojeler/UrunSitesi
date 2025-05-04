using Microsoft.AspNetCore.Mvc;

namespace UrunSitesi.WebAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            return View();
        }
    }
}
