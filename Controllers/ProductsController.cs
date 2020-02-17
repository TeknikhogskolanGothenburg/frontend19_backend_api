using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebShop.Models;
using WebShop.Repositories;

namespace WebShop.Controllers
{
    [Route("api/v1.0/Products")]
    [ApiController]
    public class ProductsController : Controller
    {
        private ProductsRepository _repository;
        public ProductsController(IConfiguration configration)
        {
            _repository = new ProductsRepository(configration);
        }

        // GET: api/v1.0/Products
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _repository.getAllProducts();
        }
        
        // GET: api/v1.0/Products/<id>
        [HttpGet("{id}")]
        public Product Get(string id)
        {
            return _repository.getProductById(id);
        }

        // POST: api/v1.0/Products
        [HttpPost]
        public void Post([FromBody]Product product)
        {
            _repository.saveProduct(product);
        }

        // DELETE: api/v1.0/Products/<id>
        [HttpDelete("{id}")]
        public void deleteProduct(string id)
        {
            _repository.deleteProduct(id);
        }

        // PUT: api/v1.0/Products/<id>
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Product product)
        {
            _repository.updateProduct(id, product);
        }
    }
}