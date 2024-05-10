namespace Catalog.Domain;

public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public required ICollection<OrderDetail> OrderDetails { get; set; }
}