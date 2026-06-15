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

    //GET: api/client/{id}
    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        return Ok(_clientsService.GetById(id));
    }

    //GET: api/clients
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetAll()
    {
        return Ok(_clientsService.GetAll());
    }

    //POST: api.clients
    [HttpPost]
    public ActionResult Create(CreateClientDto dto)
    {
        int newId = _clientsService.Create(dto);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateClientDto dto)
    {
        _clientsService.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        _clientsService.Delete(id);
        return NoContent();
    }
    
}