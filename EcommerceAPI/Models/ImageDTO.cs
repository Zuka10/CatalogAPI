namespace Ecommerce.API.Models;

public class ImageModel
{
    public required ICollection<IFormFile> Images { get; set; }
    public required int ProductId { get; set; }
}