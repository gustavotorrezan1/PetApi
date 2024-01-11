using PetApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PetDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver1")));

Console.WriteLine("Selecione o banco a ser usado \n 1 - SqlServer 1 \n 2 - SqlServer 2");
var Banco = Console.ReadLine();

switch (Banco)
{
    case "1":
        builder.Services.AddDbContext<PetDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver1")));
        break;

    case "2":
        builder.Services.AddDbContext<PetDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver2")));
        break;

    default:
        Console.WriteLine("Banco Inválido");
        break;
}



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

