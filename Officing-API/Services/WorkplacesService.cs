using Officing_API.Data;
using Officing_API.DTOs;
using Officing_API.Models;

namespace Officing_API.Services;

public class WorkplacesService : IWorkspaceService
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _clientService;

    public WorkplacesService(AppDbContext dbContext, IClientService clientService)
    {
        _dbContext = dbContext;
        _clientService = clientService;
    }

    public IEnumerable<WorkspaceDto> GetAll()
    {
        return _dbContext.Workspaces
            .Select(w => new WorkspaceDto
            {
                Id = w.Id,
                Address = $"{w.City} {w.PostalCode}, {w.Street} {w.StreetNumber}",
                DailyRate = w.DailyRate,
                Deposit = w.Deposit,
                IsAvailable = w.IsAvailable
            })
            .ToList();
    }

    public WorkspaceDto GetById(int id)
    {
        var workspace = _dbContext.Workspaces.FirstOrDefault(w => w.Id == id);

        if (workspace == null)
            throw new KeyNotFoundException($"Workspace with ID {id} not found");

        return new WorkspaceDto
        {
            Id = workspace.Id,
            Address = $"{workspace.City} {workspace.PostalCode}, {workspace.Street} {workspace.StreetNumber}",
            DailyRate = workspace.DailyRate,
            Deposit = workspace.Deposit,
            IsAvailable = workspace.IsAvailable
        };
    }

    public int Create(CreateWorkspaceDto dto, int requestorId)
    {
        if (!_clientService.IsPriviledgedUser(requestorId))
        {
            throw new UnauthorizedAccessException("You do not have permission to create workspaces.");
        }

        var exists = _dbContext.Workspaces.Any(w =>
            w.City.ToLower() == dto.City.ToLower()
            && w.Street.ToLower() == dto.Street.ToLower()
            && w.StreetNumber.ToLower() == dto.StreetNumber.ToLower()
            && w.PostalCode.ToLower() == dto.PostalCode.ToLower()
        );

        if (exists)
            throw new ApplicationException(
                $"Workplace with address: {dto.City} {dto.PostalCode}, {dto.Street} {dto.StreetNumber} already exists"
            );

        var newWorkspace = new Workspace
        {
            City = dto.City,
            Street = dto.Street,
            StreetNumber = dto.StreetNumber,
            PostalCode = dto.PostalCode,
            DailyRate = dto.DailyRate,
            Deposit = dto.Deposit ?? 0,
            IsAvailable = true,
            OwnerId = 1
        };

        _dbContext.Workspaces.Add(newWorkspace);
        _dbContext.SaveChanges();

        return newWorkspace.Id;
    }

    public void Update(int id, UpdateWorkspaceDto dto)
    {
        var workspace = _dbContext.Workspaces.FirstOrDefault(w => w.Id == id);

        if (workspace == null)
            throw new KeyNotFoundException($"Workspace with ID {id} not found");

        if (dto.DailyRate != null)
        {
            workspace.DailyRate = dto.DailyRate;
        }

        workspace.IsAvailable = dto.IsAvailable;

        _dbContext.SaveChanges();
    }

    public void Delete(int id, int requestorId)
    {
        if (!_clientService.IsAdmin(requestorId))
        {
            throw new UnauthorizedAccessException("You do not have permission to delete this workspace.");
        }

        var workspace = _dbContext.Workspaces.FirstOrDefault(w => w.Id == id);

        if (workspace == null)
            throw new KeyNotFoundException($"Workspace with ID {id} not found");

        if (!workspace.IsAvailable)
        {
            throw new ApplicationException("Can't delete workspace in use!");
        }

        _dbContext.Workspaces.Remove(workspace);
        _dbContext.SaveChanges();
    }
}