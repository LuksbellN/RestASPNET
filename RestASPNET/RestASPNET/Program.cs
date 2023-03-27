using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using RestASPNET.Business;
using RestASPNET.Business.Implementations;
using RestASPNET.Hypermedia.Enricher;
using RestASPNET.Hypermedia.FIlters;
using RestASPNET.Model.Context;
using RestASPNET.Repository.Generic;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using RestASPNET.Services;
using RestASPNET.Services.Implementations;
using RestASPNET.Repository;
using RestASPNET.Repository.Implementations;
using RestASPNET.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MySQLConnectionString");
builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(connection, new MySqlServerVersion(new Version()))
    );

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

// Add services to the container.

var filterOptions = new HypermediaFilterOptions();
filterOptions.ResponseEnrichers.Add(new PersonEnricher());
filterOptions.ResponseEnrichers.Add(new BookEnricher());

builder.Services.AddSingleton(filterOptions);

var tokenConfigurations = new TokenConfiguration();

new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("TokenConfigurations")
    )
    .Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    }));

builder.Services.AddApiVersioning();

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
}).
AddXmlSerializerFormatters();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>();
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Rest API's",
            Version = "v1",
            Description = "RestFul apis with HATEOAS, Paging, Jwts, Authentication...",
            Contact = new OpenApiContact
            {
                Name = "Lucas",
                Url = new Uri("https://github.com/LuksbellN")
            }
        });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    MigrateDataBase(connection);
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    "DefaultApi", "{controller=values}/{id?}");

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
    }
    catch (Exception ex)
    {
        Log.Error($"Database migration failed: {ex}");
        throw;
    }
}
