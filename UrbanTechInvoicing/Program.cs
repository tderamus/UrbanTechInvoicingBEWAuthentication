using Microsoft.EntityFrameworkCore;
using UrbanTechInvoicing.Data;
using UrbanTechInvoicing.Models;


var builder = WebApplication.CreateBuilder(args);
// Add configuration to read from user secrets when in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var connectionString = builder.Configuration.GetConnectionString("UrbanTechDbConnectionString");
builder.Services.AddDbContext<UrbanTechDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.

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





app.Run();


