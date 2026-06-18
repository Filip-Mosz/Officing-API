using Officing_API.Data;
using Officing_API.DTOs;
using Officing_API.Models;
using AutoMapper;

namespace Officing_API.Services;

public class ClientsService: IClientService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ClientsService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<ClientDto> GetAll()
    {
        var clients = _dbContext.Clients.ToList();
        return _mapper.Map<List<ClientDto>>(clients);
    }

    public ClientDto GetById(int id)
    {
        var client = _dbContext.Clients.FirstOrDefault(w => w.Id == id);
        if (client == null) throw new KeyNotFoundException($"Client with ID ${id} not found");
        return _mapper.Map<ClientDto>(client);
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

        var newClient = _mapper.Map<Client>(dto);
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