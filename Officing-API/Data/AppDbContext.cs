using Microsoft.EntityFrameworkCore;
using Officing_API.Models;

namespace Officing_API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();
}