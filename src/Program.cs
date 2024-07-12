using Microsoft.EntityFrameworkCore;
using Tricentis.RestApiTemplate.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DemoContext>(opt => opt.UseInMemoryDatabase("DemoItems"))
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddScoped<IDemoRepository, DemoRepository>();

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
