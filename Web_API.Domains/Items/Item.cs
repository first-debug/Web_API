using System.Collections;

namespace Web_API.Domain.Items;

public class Item(string title, ItemType type, ItemStatus status, decimal amount, string disc)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Title { get; set; } = title;
    public ItemType Type { get; set; } = type;
    public ItemStatus Status { get; set; } = status;
    public decimal Amount { get; set; } = amount;
    public string Description { get; set; } = disc;
}