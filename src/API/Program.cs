using Microsoft.EntityFrameworkCore;
using Business.Services;
using Data.Contexts;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DemoContext>(opt => opt.UseInMemoryDatabase("DemoItems"))
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddScoped<IDemoService, DemoService>();

builder.Logging.AddFilter("System", LogLevel.Information)
    .AddFilter("Microsoft", LogLevel.Information)
    .AddFilter("Microsoft", LogLevel.Information);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();