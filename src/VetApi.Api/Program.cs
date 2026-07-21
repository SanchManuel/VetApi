using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using VetApi.Api.Exceptions;
using VetApi.Infrastructure;
using Microsoft.OpenApi;
using System.Reflection;
using VetApi.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VetApi",
        Version = "v1",
        Description = "API REST para la gestión de una clínica veterinaria."
    });
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "VetApi v1");

        options.RoutePrefix = "swagger";
    });
}

app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });

app.MapHealthChecks(
    "/health/ready",
    new HealthCheckOptions { Predicate = healthCheck => healthCheck.Tags.Contains("ready") }
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
