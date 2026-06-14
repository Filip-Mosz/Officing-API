using Officing_API.Models;

namespace Officing_API.DTOs;

public class ClientDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public RoleEnum Role { get; set; } = RoleEnum.User; // only Admin can give other than user
}