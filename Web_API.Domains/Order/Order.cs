namespace Web_API.Domain.Order;

public class Order(OrderStatus status)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public List<OrderItem> OrderItems { get; } = [];
    public OrderStatus Status { get; set; } = status;
    public decimal OrderAmount { get; private set; }

    public Order(OrderStatus status, List<OrderItem> orderItems) : this(status)
    {
        OrderItems = orderItems;
        foreach (var item in orderItems)
            OrderAmount += item.Amount;
    }

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
            FirstOrDefault(orderItem => orderItem.Id == orderItemId);
        if (item == null) 
            return false;
        OrderItems.Remove(item);
        OrderAmount -= item.Amount;
        return true;
    }
}