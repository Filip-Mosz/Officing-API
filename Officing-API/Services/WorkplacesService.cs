using Officing_API.DTOs;
using Officing_API.Models;

namespace Officing_API.Services;

public class WorkplacesService: IWorkspaceService
{
    private static List<Workspace> _workspaces = new List<Workspace>
    {
        new Workspace
        {
            Id = 1, City = "Gdańsk", Street = "Grunwaldzka", StreetNumber = "1", DailyRate = 120, IsAvailable = true, OwnerId = 1
        },
        new Workspace
        {
            Id = 2, City = "Gdynia", Street = "Świętojańska", StreetNumber = "1B", DailyRate = 420, IsAvailable = true, OwnerId = 1
        }
    };

    public IEnumerable<WorkspaceDto> GetAll()
    {
        return _workspaces.Select(w => new WorkspaceDto
            {
                Id = w.Id,
                Address = $"{w.City} {w.PostalCode}, {w.Street} {w.StreetNumber}",
                DailyRate = w.DailyRate,
                Deposit = w.Deposit,
                IsAvailable = w.IsAvailable
            }
        );
    }

    public WorkspaceDto GetById(int id)
    {
        var workspace = _workspaces.FirstOrDefault(w => w.Id == id);
        if (workspace == null) throw new KeyNotFoundException($"Workspace with ID ${id}  not found");
        return new WorkspaceDto
        {
            Id = workspace.Id,
            Address = $"{workspace.City} {workspace.PostalCode}, {workspace.Street} {workspace.StreetNumber}",
            DailyRate = workspace.DailyRate,
            Deposit = workspace.Deposit,
            IsAvailable =  workspace.IsAvailable
        };
    }

    public int Create(CreateWorkspaceDto dto)
    {
        //Business rule #1: Address duplicate prevention
        var exists = _workspaces.Any(w => w.City.ToLower().Equals(dto.City.ToLower())
                                          && w.Street.ToLower().Equals(dto.Street.ToLower())
                                          && w.StreetNumber.ToLower().Equals(dto.StreetNumber.ToLower())
                                          && w.PostalCode.ToLower().Equals(dto.PostalCode.ToLower())
        );
        if (exists) throw new ApplicationException($"Workplace with address: {dto.City} {dto.PostalCode}, {dto.Street} {dto.StreetNumber} already exists");

        var newId = _workspaces.Any() ? _workspaces.Max(w => w.Id) + 1 : 1;
        var newWorkspace = new Workspace
        {
            Id = newId,
            City = dto.City,
            Street = dto.Street,
            StreetNumber = dto.StreetNumber,
            PostalCode = dto.PostalCode,
            DailyRate = dto.DailyRate,
            OwnerId = 1
        };
        _workspaces.Add(newWorkspace);
        return newId;
    }

    public void Update(int id, UpdateWorkspaceDto dto)
    {
        var workspace = _workspaces.FirstOrDefault(w => w.Id == id);
        if (workspace == null) throw new KeyNotFoundException($"Workspace with ID {id} not found");
        if (dto.DailyRate != null)
        {
            workspace.DailyRate = dto.DailyRate;
        }
        workspace.IsAvailable = dto.IsAvailable;
    }
    
    public void Delete(int id)
    {
        var workspace = _workspaces.FirstOrDefault(w => w.Id == id);
        if (workspace == null) throw new KeyNotFoundException($"Workspace with ID {id} not found");
        //Business rule #2: Can't  delete unavailable workspace
        if (!workspace.IsAvailable)
        {
            throw new KeyNotFoundException($"Can't delete workspace in use!");
        }
        _workspaces.Remove(workspace);
    }
}