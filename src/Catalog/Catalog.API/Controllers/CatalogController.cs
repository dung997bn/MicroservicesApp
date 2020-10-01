using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsById(string id)
        {
            var products = await _productRepository.GetProduct(id);
            if (products == null)
            {
                _logger.LogError($"Product with id: {id} not found");
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("[action]/{name}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var products = await _productRepository.GetProductsByName(name);
            return Ok(products);
        }

        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _productRepository.GetProductsByCategory(category);
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete(string id)
        {
            var products = await _productRepository.GetProduct(id);
            if (products == null)
            {
                _logger.LogError($"Product with id: {id} not found");
                return NotFound();
            }
            return Ok(await _productRepository.Delete(id));
        }

    }
}
