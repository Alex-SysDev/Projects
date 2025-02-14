using AutoMapper;
using E_Commerce_Product_Management.Dto;
using E_Commerce_Product_Management.Interfaces;
using E_Commerce_Product_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Product_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        public IActionResult GetOrder()
        {
            var orders = _mapper.Map<List<OrderDto>>(_orderRepository.GetOrders());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]
        public IActionResult GetOrderById(int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();

            var orderById = _mapper.Map<OrderDto>(_orderRepository.GetOrderById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orderById);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrder([FromBody] OrderWriteDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (orderCreate.OrderDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Order date cannot be in the past.");
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Order>(orderCreate);

            if (!_orderRepository.CreateOrder(orderMap))
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
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto updatedOrder)
        {
            if (updatedOrder == null)
                return BadRequest(ModelState);

            if (id != updatedOrder.Id)
                return BadRequest(ModelState);

            if (!_orderRepository.OrderExists(id))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (updatedOrder.OrderDate < DateTime.Now)
            {
                ModelState.AddModelError("", "Order date cannot be in the past.");
                return BadRequest(ModelState);
            }

            var orderMap = _mapper.Map<Order>(updatedOrder);
            if (!_orderRepository.UpdateOrder(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrder(int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();

            var orderToDelete = _orderRepository.GetOrderById(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.DeleteOrder(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting order");
            }

            return NoContent();
        }
    }
}
