namespace Catalog.API.Models;

public class OrderDetailsDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class OrderDTO
{
    public ICollection<OrderDetailsDTO> OrderDetails { get; set; } = new List<OrderDetailsDTO>();
}