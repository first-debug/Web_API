namespace Web_API.Domain.Order;

public class OrderItem(string title, decimal amount)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; init; } = title;
    public decimal Amount { get; init; } = amount;

}