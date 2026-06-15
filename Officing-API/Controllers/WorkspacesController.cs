using Microsoft.AspNetCore.Mvc;
using Officing_API.Services;
using Officing_API.DTOs;

namespace Officing_API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class WorkspacesController : ControllerBase
{
    private readonly IWorkspaceService _workspacesService;
    public WorkspacesController(IWorkspaceService workspacesService)
    {
        _workspacesService = workspacesService;
    }

    //GET: api/workspace/{id}
    [HttpGet("{id}")]
    public ActionResult<WorkspaceDto> GetById(int id)
    {
        return Ok(_workspacesService.GetById(id));
    }

    //GET: api/workspaces
    [HttpGet]
    public ActionResult<IEnumerable<WorkspaceDto>> GetAll()
    {
        return Ok(_workspacesService.GetAll());
    }

    //POST: api.workspaces
    [HttpPost]
    public ActionResult Create(CreateWorkspaceDto dto, [FromQuery] int requestorId)
    {
        int newId = _workspacesService.Create(dto , requestorId);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }

    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateWorkspaceDto dto)
    {
        _workspacesService.Update(id, dto);
        return NoContent();//returns 204 - No Content
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id, [FromQuery] int requestorId)
    {
        _workspacesService.Delete(id, requestorId);
        return NoContent();
    }
    
}