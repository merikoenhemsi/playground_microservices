using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Product.API.Data;
using Product.API.Extensions;
using Product.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnectionString")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Playground - Product HTTP API",
        Version = "v1",
        Description = "The Product Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
    });
});

var app = builder.Build();

app.MigrateDatabase<ProductContext>((context, services) =>
{
    ProductContextSeed
        .SeedAsync(context)
        .Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
