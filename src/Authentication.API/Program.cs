using Microsoft.Extensions.Configuration;
using System.Reflection;
using Authentication.DataTransferModels.Authentication;
using Authentication.Data.Extensions;
using Authentication.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.Configure<Token>(configuration.GetSection("token"));
builder.Services.AddHttpContextAccessor();
builder.Services.SetupIdentityDatabase(configuration);
builder.Services.AddAuthenticationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.EnsureIdentityDbIsCreated();
    app.SeedIdentityDataAsync().Wait();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

