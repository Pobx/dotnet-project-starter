using Microsoft.Extensions.Configuration;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

var environment = builder.Environment;
var contentRootPath = builder.Environment.ContentRootPath;
var ocelotConfigurationFolder = builder.Configuration.GetValue<string>("OcelotConfigurationFolder") ?? throw new ArgumentException("OcelotConfigurationFolder is not in appsetting.json");

builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = ocelotConfigurationFolder;
});

builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseSwaggerForOcelotUI(opt =>
{
    opt.PathToSwaggerGenerator = "/swagger/docs";
    opt.DownstreamSwaggerHeaders = new[]
  {
      new KeyValuePair<string, string>("Auth-Key", "AuthValue"),
  };
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
