namespace Ecommerce.DTO;

public class Country
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<City>? Cities { get; set; }
}