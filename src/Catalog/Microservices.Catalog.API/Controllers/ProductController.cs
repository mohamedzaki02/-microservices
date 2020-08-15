using System;
using System.Threading.Tasks;
using Microservices.Catalog.API.Entities;
using Microservices.Catalog.API.Filters;
using Microservices.Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.Catalog.API.Controllers
{

    [Route("api/v1/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository repo, ILogger<ProductController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductFilter filter)
        {
            return Ok(await _repo.GetProducts(filter));
        }


        [HttpGet("{id:length(24)}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            var product = await _repo.GetProductById(id);
            if (product == null)
            {
                _logger.LogError($"Product With ID: {id}, Not Found");
                return NotFound();
            }
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repo.Create(product);
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _repo.Update(product));
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] string id)
        {
            return Ok(await _repo.Delete(id));
        }

    }
}