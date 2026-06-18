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

    /// <summary>
    /// Returns workspace by id.
    /// </summary>
    /// <param name="id">Workspace identifier.</param>
    /// <returns>Workspace details.</returns>
    [HttpGet("{id}")]
    public ActionResult<WorkspaceDto> GetById(int id)
    {
        return Ok(_workspacesService.GetById(id));
    }

    /// <summary>
    /// Returns paginated list of workspaces.
    /// </summary>
    /// <param name="query">Pagination parameters.</param>
    /// <returns>List of workspaces.</returns>
    [HttpGet]
    public ActionResult<IEnumerable<WorkspaceDto>> GetAll([FromQuery] PaginationQuery query)
    {
        var result = _workspacesService.GetAll(query);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new workspace.
    /// </summary>
    /// <param name="dto">Workspace data.</param>
    /// <param name="requestorId">User performing the operation.</param>
    /// <returns>Created workspace id.</returns>
    [HttpPost]
    public ActionResult Create(CreateWorkspaceDto dto, [FromQuery] int requestorId)
    {
        int newId = _workspacesService.Create(dto , requestorId);
        return CreatedAtAction(nameof(GetById), new {id = newId}, dto);
    }

    /// <summary>
    /// Updates workspace data.
    /// </summary>
    /// <param name="id">Workspace identifier.</param>
    /// <param name="dto">Updated workspace data.</param>
    [HttpPut("{id}")]
    public ActionResult Update(int id, UpdateWorkspaceDto dto)
    {
        _workspacesService.Update(id, dto);
        return NoContent();//returns 204 - No Content
    }

    /// <summary>
    /// Soft deletes workspace.
    /// </summary>
    /// <param name="id">Workspace identifier.</param>
    /// <param name="requestorId">User performing the operation.</param>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id, [FromQuery] int requestorId)
    {
        _workspacesService.Delete(id, requestorId);
        return NoContent();
    }
    
}