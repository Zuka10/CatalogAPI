namespace Catalog.Domain;

public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public required ICollection<OrderDetail> OrderDetails { get; set; }
}