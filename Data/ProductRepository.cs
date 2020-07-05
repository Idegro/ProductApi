using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Data
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        private readonly ApplicationContext _context;
        public ProductRepository(IOptions<ConnectionStrings> options, ApplicationContext context) : base(options.Value.ProductDB, context, context.Product)
        {
            _context = context;
        }

        public IEnumerable<Product> GetProductList()
        {
            return base.GetAll();
        }

        public Product GetProductById(Guid id)
        {
           return base.GetById(id);
        }

        public IEnumerable<Product> GetProductByString(string search)
        {
            return base.GetByName(search);
        }

        public void UploadNewProduct(Product product)
        {
            base.Create(product);
        }

        public void UpdateExistingProduct(Guid id, Product newProd)
        {
            Product product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            
            product.Name = newProd.Name;

            base.Update(product);
        }

        public void DeleteProduct(Guid id)
        {
            Product product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            base.Delete(product);
        }
    }
}
