using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_API.DTO.OrderItems;
using Product_Management_API.Services.OrderItemsService;

namespace Product_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsService _orderItemsService;

        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemsService.GetOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemsService.GetOrderItemByIdAsync(id);
            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToOrder([FromBody] OrderItemsCreateDto dto)
        {
            var createdOrderItem = await _orderItemsService.AddItemToOrderAsync(dto);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = createdOrderItem.OrderItemId }, createdOrderItem);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemsUpdateDto dto)
        {
            await _orderItemsService.UpdateOrderItemAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            await _orderItemsService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}
