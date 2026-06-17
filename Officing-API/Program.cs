using Microsoft.EntityFrameworkCore;
using Officing_API.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Officing_API.Services.IWorkspaceService, Officing_API.Services.WorkplacesService>();
builder.Services.AddScoped<Officing_API.Services.IClientService, Officing_API.Services.ClientsService>();

var app = builder.Build();

app.UseMiddleware<Officing_API.Middleware.ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// setting up admin user if not exists
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!dbContext.Clients.Any())
    {
        dbContext.Clients.Add(new Officing_API.Models.Client
        {
            Login = "admin",
            Role = Officing_API.Models.RoleEnum.Admin
        });

        dbContext.SaveChanges();
    }
}

app.Run();