using PetAgenda.Abstractions.Repositories;
using PetAgenda.Abstractions.Repositories.Implementations;
using PetAgenda.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
        options.AddPolicy("MyPolicy",
            builder => {
                builder.WithOrigins("http://localhost:53462")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
            }

        );

});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add DataBase and Repository Services
DataBaseConnection mySqlDataBase = new(builder.Configuration.GetConnectionString("MySQLConnection"));

IRepository repo = new Repository(mySqlDataBase);

builder.Services.AddSingleton(repo);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
