using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_API.DTO.Orders;
using Product_Management_API.Services.OrderService;

namespace Product_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] int? customerId)
        {
            var orders = await _ordersService.GetAllOrdersAsync(customerId);
            return Ok(orders);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrdersCreateDto dto)
        {
            var createdOrder = await _ordersService.CreateOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrdersUpdateDto dto)
        {
            await _ordersService.UpdateOrderAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _ordersService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
