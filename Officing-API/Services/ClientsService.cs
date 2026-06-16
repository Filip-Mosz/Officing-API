using Officing_API.Data;
using Officing_API.DTOs;
using Officing_API.Models;

namespace Officing_API.Services;

public class ClientsService: IClientService
{
    //static List<Client> _clients = new List<Client>
    //{
    //    new Client
    //    {
    //        Id = 0, Login = "ToveJ",  Role = RoleEnum.Admin,
    //    },
    //    new Client
    //    {
    //        Id = 1, Login = "Snusmumriken", Role =  RoleEnum.User
    //    },
    //    new Client
    //    {
    //        Id = 2, Login = "PikkuMyy", Role =  RoleEnum.User
    //    }
    //};
    private readonly AppDbContext _dbContext;

    public ClientsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ClientDto> GetAll()
    {
        return _dbContext.Clients
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Login = c.Login,
                Role = c.Role
            })
            .ToList();
    }

    public ClientDto GetById(int id)
    {
        var client = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return new ClientDto
        {
            Id = client.Id,
            Login = client.Login,
            Role = client.Role
        };
    }

    public int Create(CreateClientDto dto, int requestorId)
    {
        if (!IsPriviledgedUser(requestorId))
        {
            throw new UnauthorizedAccessException("You do not have permission to add clients.");
        }
        //Business rule #1: Address duplicate prevention
        var exists = _dbContext.Clients.Any(c => c.Login.ToLower().Equals(dto.Login.ToLower())
        );
        if (exists) throw new ApplicationException($"Client with login: {dto.Login} already exists");

        var newClient = new Client
        {
            Login = dto.Login,
            Role = dto.Role
        };
        if (!IsAdmin(requestorId) && newClient.Role == RoleEnum.Admin)
        {
            throw new UnauthorizedAccessException("You do not have permission to create admin accounts.");
        }
        _dbContext.Clients.Add(newClient);
        _dbContext.SaveChanges();
        return newClient.Id;
    }

    public void Update(int id, UpdateClientDto dto,  int requestorId)
    {
        if (!IsAdmin(requestorId))
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this workspace.");
        }
        var client = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        client.Role = dto.Role;
        _dbContext.SaveChanges();
    }
    
    public void Delete(int id, int requestorId)
    {
        if (!IsAdmin(requestorId))
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this workspace.");
        }
        var client = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        _dbContext.Clients.Remove(client);
        _dbContext.SaveChanges();
    }

    public bool IsAdmin(int id)
    {
        var requestor = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (requestor == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return requestor.Role == RoleEnum.Admin;
    }
    
    public bool IsPriviledgedUser(int id)
    {
        var requestor = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (requestor == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return requestor.Role != RoleEnum.User;
    }
}