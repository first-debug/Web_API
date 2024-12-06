namespace Web_API.Domain.Items;

public class Item
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public ItemType Type { get; set; }
    public ItemStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}