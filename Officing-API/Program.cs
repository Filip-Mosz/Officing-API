using Microsoft.EntityFrameworkCore;
using Officing_API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();