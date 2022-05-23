using Domain.Models;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductApp
{
    public class ProductService : IProductService
    {
        private readonly DataContext _db;

        public ProductService(DataContext db)
        {
            _db = db;
        }

        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges(true);
        }

        public IQueryable<Product> GetProducts()
        {
            return _db.Products;
        }

        public void UpdateProduct(Product product)
        {
            _db.Update(product);
            _db.SaveChanges();
        }
    }
}
