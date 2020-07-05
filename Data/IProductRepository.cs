using ProductApi.Models;
using System;
using System.Collections.Generic;

namespace ProductApi.Data
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetProductList();
        public Product GetProductById(Guid id);
        public IEnumerable<Product> GetProductByString(string search);
        public void UploadNewProduct(Product product);
        public void DeleteProduct(Guid id);
        public void UpdateExistingProduct(Guid id, Product product);
    }
}