using CBTW.Application.Interfaces.Repositories;
using CBTW.Application.Interfaces.Services;
using CBTW.Application.Services.Implementations;
using CBTW.Infrastructure.Data;
using CBTW.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

using CBTW.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular app
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddDbContext<MilotoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MilotoDb")));

// Repositories

// Services
builder.Services.AddScoped<IDrawsService, DrawsService>();
builder.Services.AddScoped<IProspectService, ProspectService>();
builder.Services.AddScoped<IDrawRepository, DrawRepository>();
builder.Services.AddScoped<IProspectRepository, ProspectRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CBTW API V1");
    c.RoutePrefix = string.Empty; // Swagger como página principal
});

// Use CORS before authorization
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();





