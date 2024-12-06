using Microsoft.EntityFrameworkCore;

using Web_API.Domain.Items;
using Web_API.Infrastructure.Data;

namespace Web_API.Infrastructure.Repositories;

public class ItemRepository(Context context) : Repository(context)
{
    public async Task<Item?> GetRandomItemAsync()
    {
        return await Context.Items.FirstOrDefaultAsync();

    }
    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await Context.Items.ToListAsync();
    }
    
    public async Task<Item?> GetItemByIdAsync(Guid id)
    {
        return await Context.Items.FirstOrDefaultAsync(item => item.Id == id);
    }
    
    public async Task<List<Item>> GetItemsByTemplateAsync(string template)
    {
        return await Context.Items.Where(item => 
            item.Title.ToLower().Contains(template.ToLower())).ToListAsync();
    }
    
    public async Task AddItemAsync(Item item)
    {
        var itemInDb = await GetItemByIdAsync(item.Id);
        if (itemInDb is not null)
            throw new Exception($"Item with id: {item.Id} already exists");
        await Context.Items.AddAsync(item);
        await Context.SaveChangesAsync();
    }

    public async Task AddItemsAsync(List<Item> items)
    {
        foreach (var item in items)
            await Context.Items.AddAsync(item);
    }
    /// <summary>
    /// Update Item by id.
    /// </summary>
    /// <param name="item"></param>
    /// <returns>true - if item already exist, false - if item does not exist</returns>
    public async Task UpdateItemAsync(Item item)
    {
        var itemInDb = await GetItemByIdAsync(item.Id);
        if (itemInDb == null) 
            throw new Exception("Item not found");
        Context.Entry(itemInDb).CurrentValues.SetValues(item);
        var orderItemsInDb = await Context.OrderItem.Where(orderItem => item.Id == itemInDb.Id).ToListAsync();
        if (orderItemsInDb.Count != 0)
            foreach (var orderItemInDb in orderItemsInDb)
                Context.Entry(orderItemInDb).CurrentValues.SetValues(item);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteItemByIdAsync(Guid id)
    {
        var itemInDb = await GetItemByIdAsync(id);
        if (itemInDb == null) 
            throw new Exception($"Item with id: {id} not found");
        Context.Remove(itemInDb);
        var orderItemsInDb = await Context.OrderItem.Where(orderItem => orderItem.ItemId == id).ToListAsync();
        if (orderItemsInDb.Count != 0)
            foreach (var orderItemInDb in orderItemsInDb)
                Context.Remove(orderItemInDb);
        await Context.SaveChangesAsync();
    }
}