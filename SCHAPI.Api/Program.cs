using Microsoft.EntityFrameworkCore;
using SCHAPI.Api.Util;
using SCHAPI.Application.Extensions;
using SCHAPI.Infrastructure.Persistences.Contexts;
using SCHAPI.Infrastructure.Persistences.Interfaces;
using SCHAPI.Infrastructure.Persistences.Repositories;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var cors = "Cors";

builder.Services.AddDbContext<SCHAPIContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("SqlServer"), s =>
    {
        s.MigrationsAssembly("SCHAPI.Infrastructure");
        s.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddIjectionApplication();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors(cors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await app.Seed();
}

app.UseWatchDogExceptionLogger();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWatchDog(config =>
{
    config.WatchPageUsername = configuration.GetSection("WatchDogOptions:UserName").Value;
    config.WatchPagePassword = configuration.GetSection("WatchDogOptions:Password").Value;
});

app.Run();
