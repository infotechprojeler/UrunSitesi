using Microsoft.AspNetCore.Mvc;
using UrunSitesi.Core.Entities;
using UrunSitesi.Service;

namespace UrunSitesi.MVCWebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IService<Product> _productService;

        public CartController(IService<Product> productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var product = _productService.Find(productId);
            if (product != null)
            {

            }
            return View();
        }
    }
}
