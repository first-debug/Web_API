using Microsoft.EntityFrameworkCore;
using Web_API.Domain.Order;
using Web_API.Infrastructure.Data;

namespace Web_API.Infrastructure.Repositories;

public class OrderRepository(Context context) : Repository(context)
{
    public async Task<Order?> GetOrderByIdAsync(Guid orderId)
    {
        return await Context.Orders
            .Where(order => order.Id == orderId)
            .Include(order => order.OrderItems)
            .FirstOrDefaultAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        var orderInDb = await GetOrderByIdAsync(order.Id);
        if (orderInDb is not null)
            throw new Exception("Order already exists");
        foreach (var orderItem in order.OrderItems)
            if (await Context.Items.FirstOrDefaultAsync(item => item.Id == orderItem.ItemId) is null)
                throw new Exception($"Item with id: {orderItem.Id} does not exist");
        await Context.Orders.AddAsync(order);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        var orderInDb = await GetOrderByIdAsync(order.Id);
        if (orderInDb is null)
            throw new Exception("Order not found");
        foreach (var orderItem in order.OrderItems)
            if (
                await Context.Items.FirstOrDefaultAsync(itemInDb => 
                        itemInDb.Id == orderItem.ItemId &&
                        itemInDb.Title == orderItem.Title &&
                        itemInDb.Amount == orderItem.Amount
                ) is null)
                throw new Exception($"Item with id: {orderItem.Id} does not exist");
        Context.Entry(orderInDb).CurrentValues.SetValues(order);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteOrderByIdAsync(Guid orderId)
    {
        var orderInDb = await GetOrderByIdAsync(orderId);
        if (orderInDb is null)
            throw new Exception($"Order with id: {orderId} not found");
        Context.Remove(orderInDb);
        await Context.SaveChangesAsync();
    }
}