using System.ComponentModel.DataAnnotations;

namespace Web_API.Domain.Order;

public class OrderItem
{
    [Key]
    public int Id { get; set; }
    public Guid ItemId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public Guid OrderId { get; set; }

}