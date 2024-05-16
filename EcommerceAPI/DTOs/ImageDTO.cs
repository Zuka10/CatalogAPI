namespace Catalog.API.DTOs;

public record ImageDTO(ICollection<IFormFile> Images, int ProductId) { }