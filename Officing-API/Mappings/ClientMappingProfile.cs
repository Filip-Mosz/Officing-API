using AutoMapper;
using Officing_API.DTOs;
using Officing_API.Models;

namespace Officing_API.Mappings;

public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        CreateMap<Client, ClientDto>();
        CreateMap<CreateClientDto, Client>();
    }
}