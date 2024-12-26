using Serilog;
using VillaAPI.CustomLogging;

var builder = WebApplication.CreateBuilder(args);

// Add serv ices to the container.
Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Error().WriteTo
                 .File("log/VillaLogging.txt",rollingInterval:RollingInterval.Day)
                 .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddScoped<ILogging,Logging>();
builder.Services.AddControllers().AddNewtonsoftJson();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
