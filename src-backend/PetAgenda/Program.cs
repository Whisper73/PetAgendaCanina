
using Microsoft.Extensions.DependencyInjection;
using PetAgenda.Abstractions.Repositories;
using PetAgenda.Abstractions.Repositories.Implementations;
using PetAgenda.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DataBaseConnection mySqlDataBase = new(builder.Configuration.GetConnectionString("MySQLConnection"));

builder.Services.AddSingleton(mySqlDataBase);

IRepository repo = new Repository(mySqlDataBase);

builder.Services.AddSingleton(repo);

//builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
