using Microsoft.AspNetCore.Identity;

namespace Catalog.Domain;

public class ApplicationUser : IdentityUser 
{ 
    public ICollection<Order>? Orders { get; set; }
}