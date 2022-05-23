using Application.ProductApp;
using Domain.Dtos;
using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private IProductService productService;
        public CartModel(IProductService productService)
        {
            this.productService = productService;
        }
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = productService.GetProducts().FirstOrDefault(p => p.ProductID == productId);
            var cartSesston = HttpContext.Session.Get("cart");
            if (cartSesston != null)
            {
                Cart = JsonSerializer.Deserialize<Cart>(cartSesston);
            }
            if (Cart == null)
            {
                Cart = new Cart();
            }

            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(product, 1);
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.RemoveLine(Cart.Lines.First(cl =>
            cl.Product.ProductID == productId).Product);
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
