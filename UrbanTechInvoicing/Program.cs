using Microsoft.EntityFrameworkCore;
using UrbanTechInvoicing.Data;
using UrbanTechInvoicing.Interfaces;
using UrbanTechInvoicing.Services;
using UrbanTechInvoicing.Repositories;
using UrbanTechInvoicing.Endpoints;


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
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddScoped<IPaymentsService, PaymentsService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();


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




app.MapCustomerEndpoints();
app.MapPaymentsEndpoints();
app.MapProductEndpoints();
app.MapServiceEndpoints();
app.Run();


