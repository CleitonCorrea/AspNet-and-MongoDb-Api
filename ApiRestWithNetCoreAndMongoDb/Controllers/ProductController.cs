using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ApiRestWithNetCoreAndMongoDb.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       public readonly Repository.IProductRepository _productRepository;
        public ProductController(Repository.IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Entities.Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            await _productRepository.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Entities.Product product)
        {
            if (product == null || id != product.Id)
            {
                return BadRequest();
            }
            await _productRepository.UpdateAsync(id, product);
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var exists = await _productRepository.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(id);
            return Ok(new {Sucess = true, Message = "Produc deleted!"}); ;
        }
    }
}
