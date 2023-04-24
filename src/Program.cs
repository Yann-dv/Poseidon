using System.Reflection;
using PoseidonApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using PoseidonApi.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.TryAddEnumerable(
    ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());
LoggerProviderOptions.RegisterProviderOptions
    <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

builder.Services.AddDbContext<ApiDbContext>(opt =>
    opt.UseInMemoryDatabase("ApiList"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0");
    options.RoutePrefix = string.Empty;
});*/

app.UseHttpsRedirection();

app.UseAuthorization();



app.MapControllers();

app.Run();