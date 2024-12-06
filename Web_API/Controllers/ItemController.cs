using Microsoft.AspNetCore.Mvc;
using Web_API.Domain.Items;
using Web_API.Infrastructure.Data;
using Web_API.Infrastructure.Repositories;

namespace Web_API.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    // private readonly Context _context;
    private readonly ItemRepository _itemRepo;
    private readonly ILogger<ItemController> _logger;
    
    
    public ItemController(Context context, ILogger<ItemController> logger)
    {
        // _context = context;
        _logger = logger;
        _itemRepo = new ItemRepository(context);
    }
    
    [HttpGet("ping")]
    public ActionResult Ping()
    {
        return Ok("Pong");
    }

    [HttpGet("random")]
    public async Task<ActionResult<Item>> GetRandomItem()
    {
        var response = await _itemRepo.GetRandomItemAsync();
        return response is null ? NotFound() : Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Item>> GetItemById(Guid id)
    {
        var response = await _itemRepo.GetItemByIdAsync(id);
        return response is null ? NotFound("Item not found") : Ok(response);
    }

    [HttpGet("find/{template}")]
    public async Task<ActionResult<List<Item>>> GetItemsByTemplate(string template)
    {
        return await _itemRepo.GetItemsByTemplateAsync(template);
    }

    [HttpPost("add")]
    public async Task<ActionResult> AddItem(Item item)
    {
        try 
        {
            await _itemRepo.AddItemAsync(item);
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
        
        return Ok();
    }

    [HttpPost("update")]
    public async Task<ActionResult> UpdateItem(Item item)
    {
        try
        {
            await _itemRepo.UpdateItemAsync(item);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        
        return Ok();
    }

    [HttpDelete("delete/{id:guid}")]
    public async Task<ActionResult> DeleteItem(Guid id)
    {
        try
        {
            await _itemRepo.DeleteItemByIdAsync(id);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        
        return Ok();
    }
}