using Officing_API.DTOs;

namespace Officing_API.Services;

public interface IClientService
{
    IEnumerable<ClientDto> GetAll();
    ClientDto GetById(int id);
    int Create(CreateClientDto dto);
    void Update(int id, UpdateClientDto dto);
    void Delete(int id);
}