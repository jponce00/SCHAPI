using Microsoft.EntityFrameworkCore;
using SCHAPI.Infrastructure.Persistences.Contexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<SCHAPIContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServer"), s =>
    {
        s.MigrationsAssembly("SCHAPI.Infrastructure");
        s.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
