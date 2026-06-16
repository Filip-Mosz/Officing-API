using Officing_API.DTOs;

namespace Officing_API.Services;

public interface IClientService
{
    IEnumerable<ClientDto> GetAll();
    ClientDto GetById(int id);
    int Create(CreateClientDto dto, int requestorId);
    void Update(int id, UpdateClientDto dto, int requestorId);
    void Delete(int id,  int requestorId);
    bool IsAdmin(int id);
    bool IsPriviledgedUser(int id);
}