﻿namespace Ecommerce.DTO;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal UnitPrice { get; set; }

    public IList<string>? Images { get; set; } = new List<string>();
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}