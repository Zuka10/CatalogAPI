namespace Catalog.API.DTOs;

public record OrderDetailsDTO(int ProductId, int Quantity) { }

public record OrderDTO()
{
    public ICollection<OrderDetailsDTO> OrderDetails { get; set; } = new List<OrderDetailsDTO>();
}