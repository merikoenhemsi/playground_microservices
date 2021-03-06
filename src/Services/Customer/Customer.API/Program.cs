using Customer.API;
using Customer.API.Data;
using Customer.API.Extensions;
using Customer.API.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<CustomerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerConnectionString")));

builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

var eventBusSettings = new EventBusSettings();
builder.Configuration.Bind(nameof(EventBusSettings), eventBusSettings);
builder.Services.AddSingleton(eventBusSettings);

builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(eventBusSettings.HostAddress);
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Playground - Customer HTTP API",
        Version = "v1",
        Description = "The Customer Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
    });
});

var app = builder.Build();

app.MigrateDatabase<CustomerContext>((context, services) =>
{
    CustomerContextSeed
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
