using Microsoft.Extensions.Configuration;
using MMLib.Ocelot.Provider.AppConfiguration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

var environment = builder.Environment;
var contentRootPath = builder.Environment.ContentRootPath;
var routesDirectory = builder.Configuration.GetValue<string>("Ocelot:Routes") ?? throw new ArgumentException("Routes is not in appsetting.json");

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routesDirectory;
});

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddOcelot().AddAppConfiguration();


var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
    opt.DownstreamSwaggerHeaders = new[]
  {
      new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
  };
});


app.UseOcelot().Wait();

app.MapControllers();

app.Run();
