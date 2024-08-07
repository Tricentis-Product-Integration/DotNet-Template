using DAL;
using Business;
using Services.Middlewares;
using Instrumentation;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCors()
    .AddHttpContextAccessor()
    .AddBusinessServices()
    .AddInstrumentation()
    .AddDALServices();


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

app.UseCors();

app.AddMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();