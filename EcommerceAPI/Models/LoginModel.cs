using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models;

public class LoginModel
{
    [Required(ErrorMessage = "User Name is required")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8), MaxLength(16)]
    public required string Password { get; set; }
}
