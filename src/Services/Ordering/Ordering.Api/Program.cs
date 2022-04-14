using MassTransit;
using Microsoft.OpenApi.Models;
using Ordering.Api;
using Ordering.Api.Extensions;
using Ordering.Core;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var eventBusSettings = new EventBusSettings();
builder.Configuration.Bind(nameof(EventBusSettings), eventBusSettings);
builder.Services.AddSingleton(eventBusSettings);

builder.Services.AddMassTransit(config => {
    config.UsingRabbitMq((ctx, cfg) => {
        cfg.Host(eventBusSettings.HostAddress);
       // cfg.
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Playground - Order HTTP API",
        Version = "v1",
        Description = "The Order Microservice HTTP API. This is a CQRS-Clean Architecture microservice sample"
    });
});

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    OrderContextSeed
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
