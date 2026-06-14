using Microsoft.AspNetCore.Mvc;
using Officing_API.Models;
using Officing_API.DTOs;

namespace Officing_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientsController : ControllerBase
{
    
    private static List<Client> _clients = new List<Client>
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

    //GET: api/client/{id}
    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        var client = _clients.FirstOrDefault(v => v.Id == id);
        if (client == null) return NotFound("Nie znaleziono takiego klienta!");
        var dto = new ClientDto
        {
            Id = client.Id,
            Login = client.Login,
            Role = client.Role
        };
        return Ok(dto);
    }

    //GET: api/clients
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetAll()
    {
        var dtos = _clients.Select(c => new ClientDto()
        {
            Id = c.Id,
            Login = c.Login,
            Role =  c.Role
        });
        return Ok(dtos);
    }

    //POST: api.clients
    [HttpPost]
    public ActionResult Create(CreateClientDto dto)
    {
        
        if (!Enum.IsDefined(typeof(RoleEnum), dto.Role))
        {
            return BadRequest("Nieprawidłowa rola.");
        }
        
        int newId = _clients.Any()? _clients.Max(v => v.Id)+1: 0;

        var newClient = new Client
        {
            Id = newId,
            Login = dto.Login,
            Role = dto.Role
        };
        
        _clients.Add(newClient);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }
    
}