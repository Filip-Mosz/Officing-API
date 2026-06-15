using Officing_API.DTOs;
using Officing_API.Models;

namespace Officing_API.Services;

public class ClientsService: IClientService
{
    static List<Client> _clients = new List<Client>
    {
        new Client
        {
            Id = 0, Login = "ToveJ",  Role = RoleEnum.Admin,
        },
        new Client
        {
            Id = 1, Login = "Snusmumriken", Role =  RoleEnum.User
        },
        new Client
        {
            Id = 2, Login = "PikkuMyy", Role =  RoleEnum.User
        }
    };

    public IEnumerable<ClientDto> GetAll()
    {
        return _clients.Select(w => new ClientDto
            {
                Id = w.Id,
                Login = w.Login,
                Role = w.Role
            }
        );
    }

    public ClientDto GetById(int id)
    {
        var client = _clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return new ClientDto
        {
            Id = client.Id,
            Login = client.Login,
            Role = client.Role
        };
    }

    public int Create(CreateClientDto dto)
    {
        //Business rule #1: Address duplicate prevention
        var exists = _clients.Any(c => c.Login.ToLower().Equals(dto.Login.ToLower())
        );
        if (exists) throw new ApplicationException($"Client with login: {dto.Login} already exists");

        var newId = _clients.Any() ? _clients.Max(w => w.Id) + 1 : 1;
        var newClient = new Client
        {
            Id = newId,
            Login = dto.Login,
            Role = dto.Role
        };
        _clients.Add(newClient);
        return newId;
    }

    public void Update(int id, UpdateClientDto dto)
    {
        var client = _clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        client.Role = dto.Role;
    }
    
    public void Delete(int id)
    {
        var client = _clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        //todo Business rule #3: Can't delete clients if not Admin
        // if (requestor.IsAvailable)
        // {
        //     throw new KeyNotFoundException($"Only administrators are allowed to delete clients!");
        // }
        _clients.Remove(client);
    }

    public static bool IsAdmin(int id)
    {
        var requestor = _clients.FirstOrDefault(w => w.Id == id);
        if (requestor == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return requestor.Role == RoleEnum.Admin;
    }
    
    public static bool NotUser(int id)
    {
        var requestor = _clients.FirstOrDefault(w => w.Id == id);
        if (requestor == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return requestor.Role != RoleEnum.User;
    }
}