using EvolveDb;
using Microsoft.EntityFrameworkCore;
using RestASPNET.Business;
using RestASPNET.Business.Implementations;
using RestASPNET.Model.Context;
using RestASPNET.Repository.Generic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");
builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version()))
    );

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
// Add services to the container.


builder.Services.AddApiVersioning();

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    MigrateDataBase(connection);
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
void MigrateDataBase(string connection)
{
    try
    {
        var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
        var evolve = new Evolve(
            evolveConnection, msg => Log.Information(msg))
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true
        };
        evolve.Migrate();
    } catch (Exception ex)
    {
        Log.Error($"Database migration failed: {ex}");
        throw;
    }
}
