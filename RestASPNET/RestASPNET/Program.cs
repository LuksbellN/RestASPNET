using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RestASPNET.Model.Context;
using RestASPNET.Services;
using RestASPNET.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySQLConnectionString"), 
                        new MySqlServerVersion(new Version()))
    );

// Add services to the container.

builder.Services.AddScoped<IPersonService, PersonServiceImplementation>();
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
