using Officing_API.Models;

namespace Officing_API.DTOs;

public class CreateClientDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public RoleEnum Role { get; set; } = RoleEnum.User;
}