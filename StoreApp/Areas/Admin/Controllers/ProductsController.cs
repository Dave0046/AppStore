#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Application.ProductApp;
using Microsoft.AspNetCore.Authorization;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin, user")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [Route("/products")]
        [Route("/admin/products")]
        public IActionResult Index()
        {
            var products = _productService.GetProducts().ToListAsync().Result;
            return View(products);
        }

        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProducts()
                .FirstOrDefaultAsync(m => m.ProductID == id).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Description,Price,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProducts().FirstOrDefaultAsync(p => p.ProductID == id).Result;
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(long id, [Bind("ProductID,Name,Description,Price,Category")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productService.UpdateProduct(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProducts()
                .FirstOrDefaultAsync(m => m.ProductID == id).Result;
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var product = _productService.GetProducts()
                .FirstOrDefaultAsync(m => m.ProductID == id).Result;
            _productService.DeleteProduct(product);
            return RedirectToAction(nameof(Index));
        }
    }
}
