namespace Officing_API.Models;

public class Client
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public RoleEnum Role { get; set; } = RoleEnum.User;
}