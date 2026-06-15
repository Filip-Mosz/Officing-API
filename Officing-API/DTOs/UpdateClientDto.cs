using System.ComponentModel.DataAnnotations;
using Officing_API.Models;

namespace Officing_API.DTOs;

public class UpdateClientDto
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;
    [Required(ErrorMessage = "Role is required")]
    public RoleEnum Role { get; set; } = RoleEnum.User;
}