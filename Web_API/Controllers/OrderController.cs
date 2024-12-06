using Microsoft.AspNetCore.Mvc;
using Web_API.Domain.Order;
using Web_API.Infrastructure.Data;
using Web_API.Infrastructure.Repositories;

namespace Web_API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepo;
    private readonly ILogger _logger;
    
    public OrderController(Context context, ILogger<OrderController> logger)
    {
        _orderRepo = new OrderRepository(context);
        _logger = logger;
    }

    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Pong");
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        var response = await _orderRepo.GetOrderByIdAsync(id);
        if (response == null)
            return NotFound();
        return Ok(response);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddOrder(Order order)
    {
        try
        {
            await _orderRepo.AddOrderAsync(order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }

    [HttpPost("update")]
    public async Task<ActionResult> UpdateOrder(Order order)
    {
        try
        {
            await _orderRepo.UpdateOrderAsync(order);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> DeleteOrder(Guid id)
    {
        try
        {
            await _orderRepo.DeleteOrderByIdAsync(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }

        return Ok();
    }
}