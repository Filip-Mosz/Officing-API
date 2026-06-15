using Officing_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Officing_API.DTOs;

public class CreateClientDto
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;
    [Required(ErrorMessage = "Role is required")]
    public RoleEnum Role { get; set; } = RoleEnum.User;
}