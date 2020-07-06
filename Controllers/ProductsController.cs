using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository, ApplicationContext context)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productRepository.GetProductList();
        }

        [HttpGet("search:{searchstring}")]
        public IEnumerable<Product> GetProductBySearchString(string searchstring)
        {
            //add filter string instead
            return _productRepository.GetProductByString(searchstring);
        }

        [HttpGet("{id}")]
        public Product GetProductById(string id)
        {
            Guid guid = Guid.Parse(id);
            return _productRepository.GetProductById(guid);
        }

        [HttpDelete("{id}")]
        public void DeleteProductById(string id)
        {
            var productId = Guid.Parse(id);
            _productRepository.DeleteProduct(productId);
        }

        [HttpPost]
        public void UploadNewProduct([FromBody] Product[] products)
        {
            foreach (var product in products)
            {
                _productRepository.UploadNewProduct(product);
            }
        }


        [HttpPut("{id}")]
        public void UpdateProductById(string id, [FromBody] Product newProduct)
        {
            var productId = Guid.Parse(id);

            _productRepository.UpdateExistingProduct(productId, newProduct);
        }
    }
}