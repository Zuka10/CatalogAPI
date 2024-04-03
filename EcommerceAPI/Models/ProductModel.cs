namespace Ecommerce.API.Models;

public class ProductModel
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int CategoryId { get; set; }
    public ICollection<IFormFile>? Images { get; set; }
}