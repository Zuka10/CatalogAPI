namespace Catalog.API.DTOs;

public record ProductDTO(string Name, string? Description, decimal UnitPrice, int CategoryId, ICollection<IFormFile>? Images) { }