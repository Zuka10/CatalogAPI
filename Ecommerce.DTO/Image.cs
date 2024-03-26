namespace Ecommerce.DTO;

public class Image
{
    public int Id { get; set; }
    public required byte[] ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}