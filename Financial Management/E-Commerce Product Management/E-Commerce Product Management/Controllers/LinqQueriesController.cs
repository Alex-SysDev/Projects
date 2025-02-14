using AutoMapper;
using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Product_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinqQueriesController : Controller
    {
        private readonly ILinqQueries _linqRepository;
        private readonly IMapper _mapper;

        public LinqQueriesController(ILinqQueries linqRepository, IMapper mapper)
        {
            _linqRepository = linqRepository;
            _mapper = mapper;
        }

        [HttpGet("category/{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        public IActionResult GetProductsBasedOnCategory(int id)
        {
            if (!_linqRepository.CategoryExists(id))
                return NotFound();

            var productsByCategory = _mapper.Map<List<ProductDto>>(_linqRepository.GetProductsBasedOnCategory(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(productsByCategory);
        }

        [HttpGet("last-month")]
        public IActionResult GetOrderWithinLastMonth()
        {
            var orderLastMonth = _mapper.Map<List<OrderDto>>(_linqRepository.GetOrderWithinLastMonth());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderLastMonth);
        }

        [HttpGet("{id}/total-sales")]
        public IActionResult GetTotalSalesOfProduct(int id)
        {
            if (!_linqRepository.ProductExists(id))
                return NotFound();

            var totalSales = _mapper.Map<List<ProductSalesDto>>(_linqRepository.GetTotalSalesOfProduct(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(totalSales);
        }

        [HttpGet("top-five")]
        public IActionResult GetTopFiveProducts()
        {

            var topFive = _mapper.Map<List<ProductSalesDto>>(_linqRepository.GetTopFiveProducts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(topFive);
        }
    }
}
