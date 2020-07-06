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
        public ProductRepository(ApplicationContext context) : base(context, context.Product)
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

            product = UpdateProductProperty(product, newProd);

            base.Update(product);
        }

        public void DeleteProduct(Guid id)
        {
            Product product = _context.Product.Where(p => p.Id == id).FirstOrDefault();
            base.Delete(product);
        }


        private Product UpdateProductProperty(Product oldProd, Product newProd)
        {
            if (newProd.Name != null)
            {
                oldProd.Name = newProd.Name;
            }

            if (newProd.Currency != null)
            {
                oldProd.Currency = newProd.Currency;
            }

            if (newProd.Price != null)
            {
                oldProd.Price = newProd.Price;
            }

            if (newProd.ProductGroupId != null)
            {
                oldProd.ProductGroupId = newProd.ProductGroupId;
            }

            return oldProd;
        }
    }
}
