namespace Web_API.Domain.Order;

public class Order
{
    public Guid Id { get; init; }
    public List<OrderItem> OrderItems { get; set;  } = [];
    public OrderStatus Status { get; set; }
    public decimal OrderAmount { get; set; }
 public bool AddOrderItem(OrderItem orderItem)
    {
        OrderItems.Add(orderItem);
        OrderAmount += orderItem.Amount;
        return true;
    }

    public bool RemoveOrderItem(OrderItem orderItem)
    {
        if (!OrderItems.Contains(orderItem))
            return false;
        OrderItems.Remove(orderItem);
        OrderAmount -= orderItem.Amount;
        return true;
    }

    public bool RemoveOrderItemById(Guid orderItemId)
    {
        OrderItem? item = OrderItems.
            FirstOrDefault(orderItem => orderItem.ItemId == orderItemId);
        if (item == null) 
            return false;
        OrderItems.Remove(item);
        OrderAmount -= item.Amount;
        return true;
    }
}