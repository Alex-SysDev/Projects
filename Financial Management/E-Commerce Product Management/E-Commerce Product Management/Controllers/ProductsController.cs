using AutoMapper;
using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Product_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        public IActionResult GetProductById(int id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var productById = _mapper.Map<ProductDto>(_productRepository.GetProductById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productById);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductWriteDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            var product = _productRepository.GetProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd())
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productCreate.Price <= 0)
            {
                ModelState.AddModelError("", "Invalid Price");
                return BadRequest(ModelState);
            }
            if (productCreate.StockQuantity <= 0)
            {
                ModelState.AddModelError("", "Invalid Stock Quantity");
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Product>(productCreate);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto updatedProduct)
        {
            if (updatedProduct == null)
                return BadRequest(ModelState);

            if (id != updatedProduct.Id)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updatedProduct.Price <= 0)
            {
                ModelState.AddModelError("", "Invalid Price");
                return BadRequest(ModelState);
            }
            if (updatedProduct.StockQuantity <= 0)
            {
                ModelState.AddModelError("", "Invalid Stock Quantity");
                return BadRequest(ModelState);
            }

            var productMap = _mapper.Map<Product>(updatedProduct);
            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong updating product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var productToDelete = _productRepository.GetProductById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
            }

            return NoContent();
        }
    }
}
