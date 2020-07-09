using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using ProductApi.Data;
using ProductApi.Models;
using ProductApi.Repositories;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _log;
        private readonly IRequestResult _result;

        public ProductsController(IUnitOfWork unitOfWork, ApplicationContext context, ILogger<ProductsController> log, IRequestResult result)
        {
            _unitOfWork = unitOfWork;
            _log = log;
            _result = result;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _unitOfWork.productRepository.GetAll();
        }

        [HttpGet("name:{searchstring}")]
        public IEnumerable<Product> GetProductBySearchString(string searchstring)
        {
            //add filter string instead
            return _unitOfWork.productRepository.GetByName(searchstring);
        }

        [HttpGet("{id}")]
        public Product GetProductById(string id)
        {
            Guid guid = Guid.Parse(id);
            return _unitOfWork.productRepository.GetById(guid);
        }

        [HttpDelete("{id}")]
        public IRequestResult DeleteProductById(string id)
        {
            var guid = Guid.Parse(id);
            try
            {
                _unitOfWork.productRepository.Delete(guid);
                _result.Success = _unitOfWork.Commit();
                return _result;
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex, ex.InnerException);
                _result.Message = ex.Message;
                _result.Success = _unitOfWork.RollBack();
                return _result;
            }
        }

        [HttpPost]
        public IRequestResult UploadNewProduct([FromBody] Product[] products)
        {
            List<IRequestResult> results = new List<IRequestResult>();

            foreach (var product in products)
            {
                RequestResult result = new RequestResult();
                try
                {
                    _unitOfWork.productRepository.Create(product);
                    result.Success = _unitOfWork.Commit();
                    results.Add(result);
                }
                catch (Exception ex)
                {

                    _log.LogError(ex.Message, ex, ex.InnerException);
                    result.Message = $" Insertion of {product.Id} failed. Reason: " + ex.Message;
                    result.Success = false;
                    results.Add(result);
                    _unitOfWork.RemoveFromContext(product);
                }
            }

            if (!results.All(r => r.Success == true))
            {
                _result.Success = false;
                _result.Message = "Possible partial failure. Not all products could be added. Details: ";

                foreach (var result in results)
                {
                    if (result.Success == false)
                    {
                        string concat = String.Concat(_result.Message, result.Message);
                        _result.Message = concat;
                    }
                }
                _unitOfWork.RollBack();
                return _result;
            }

            return _result;
        }


        [HttpPut("{id}")]
        public IRequestResult UpdateProductById(string id, [FromBody] Product newProduct)
        {
            try
            {
                _unitOfWork.productRepository.Update(newProduct);
                _result.Success = _unitOfWork.Commit();
                return _result;
                
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, ex, ex.InnerException);
                _result.Message = ex.Message;
                _result.Success = _unitOfWork.RollBack();
            }

            return _result;
        }
    }
}