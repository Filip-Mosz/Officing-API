using Officing_API.DTOs;

namespace Officing_API.Services;

public interface IWorkspaceService
{
    IEnumerable<WorkspaceDto> GetAll();
    WorkspaceDto GetById(int id);
    int Create(CreateWorkspaceDto dto,  int requestorId);
    void Update(int id, UpdateWorkspaceDto dto);
    void Delete(int id, int requestorId);
}