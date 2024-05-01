using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "User Name is required")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public required string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8), MaxLength(16)]
    [DataType(DataType.Password)]
    public required string? Password { get; set; }
}
