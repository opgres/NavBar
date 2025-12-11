using Microsoft.EntityFrameworkCore;
using NavBar.DB;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();  // добавляем поддержку контроллеров

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.MapControllers();



app.Run();
