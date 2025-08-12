using Microsoft.AspNetCore.Mvc;
using MongoDbExample.Models;
using MongoDbExample.Services;

namespace MongoDbExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<Product>> Get() =>
            await _productService.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            return product is null ? NotFound() : product;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product newProduct)
        {
            await _productService.CreateAsync(newProduct);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product updatedProduct)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();

            updatedProduct.Id = product.Id;
            await _productService.UpdateAsync(id, updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null) return NotFound();

            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("category/{category}")]
        public async Task<List<Product>> GetByCategory(string category) =>
            await _productService.GetByCategoryAsync(category);

        [HttpGet("instock")]
        public async Task<List<Product>> GetInStock() =>
            await _productService.GetInStockAsync();
    }
}
