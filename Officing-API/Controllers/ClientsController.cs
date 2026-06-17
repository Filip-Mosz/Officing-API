using Microsoft.AspNetCore.Mvc;
using Officing_API.Services;
using Officing_API.DTOs;

namespace Officing_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ClientsController : ControllerBase
{

    private readonly IClientService _clientsService;
    public ClientsController(IClientService clientsService)
    {
        _clientsService = clientsService;
    }

    /// <summary>
    /// Returns client by id.
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        return Ok(_clientsService.GetById(id));
    }

    /// <summary>
    /// Returns all clients.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetAll()
    {
        return Ok(_clientsService.GetAll());
    }

    /// <summary>
    /// Creates a new client.
    /// </summary>
    [HttpPost]
    public ActionResult Create(CreateClientDto dto, [FromQuery] int requestorId)
    {
        int newId = _clientsService.Create(dto, requestorId);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }

    /// <summary>
    /// Updates client role.
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateClientDto dto, [FromQuery] int requestorId)
    {
        _clientsService.Update(id, dto, requestorId);
        return NoContent();
    }

    /// <summary>
    /// Deletes client.
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id, [FromQuery] int requestorId)
    {
        _clientsService.Delete(id, requestorId);
        return NoContent();
    }
    
}