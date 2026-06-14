using Microsoft.AspNetCore.Mvc;
using Officing_API.Models;
using Officing_API.DTOs;

namespace Officing_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WorkspacesController : ControllerBase
{
    private static List<Workspace> _workspaces = new List<Workspace>
    {
        new Workspace
        {
            Id = 1, City = "Gdańsk", Street = "Grunwaldzka", StreetNumber = "1", Price = 120, IsAvailable = true, OwnerId = 1
        },
        new Workspace
        {
            Id = 2, City = "Gdynia", Street = "Świętojańska", StreetNumber = "1B", Price = 420, IsAvailable = true, OwnerId = 1
        }
    };

    //GET: api/workspace/{id}
    [HttpGet("{id}")]
    public ActionResult<WorkspaceDto> GetById(int id)
    {
        var workspace = _workspaces.FirstOrDefault(v => v.Id == id);
        if (workspace == null) return NotFound("Nie znaleziono takiego lokalu!");
        var dto = new WorkspaceDto
        {
            Id = workspace.Id,
            Address = $"{workspace.City} {workspace.PostalCode}, {workspace.Street} {workspace.StreetNumber}",
            DailyRate = workspace.Price,
            Deposit = workspace.Deposit
        };
        return Ok(dto);
    }

    //GET: api/workspaces
    [HttpGet]
    public ActionResult<IEnumerable<WorkspaceDto>> GetAll()
    {
        var dtos = _workspaces.Select(w => new WorkspaceDto()
        {
            Id = w.Id,
            Address = $"{w.City} {w.PostalCode}, {w.Street} {w.StreetNumber}",
            DailyRate =  w.Price
        });
        return Ok(dtos);
    }

    //POST: api.workspaces
    [HttpPost]
    public ActionResult Create(CreateWorkspaceDto dto)
    {
        int newId = _workspaces.Any()? _workspaces.Max(v => v.Id)+1: 0;

        var newWorkspace = new Workspace
        {
            Id = newId,
            City = dto.City,
            Street = dto.Street,
            StreetNumber = dto.StreetNumber,
            PostalCode = dto.PostalCode,
            Price = dto.DailyRate,
            IsAvailable = true,
            OwnerId = 1 //as test same owner all the time
        };
        
        _workspaces.Add(newWorkspace);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }
    
}