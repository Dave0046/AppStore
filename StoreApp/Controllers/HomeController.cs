using Application.ProductApp;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View(productService.GetProducts());
        }
    }
}
