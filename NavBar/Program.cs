using Microsoft.EntityFrameworkCore;
using NavBar.DB;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


builder.Services.AddControllers();  // добавляем поддержку контроллеров

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



string connection = config.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));


var loggerConfig = new LoggerConfiguration().ReadFrom.Configuration(config);

Log.Logger = loggerConfig.CreateBootstrapLogger();
var loggingBuilder = builder.Logging;
//loggingBuilder
//    .ClearProviders()
//    .AddSerilog()
//    .AddConfiguration(config);

builder.Services.AddSerilog();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.MapControllers();



app.Run();
