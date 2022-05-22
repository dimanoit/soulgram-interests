using MongoDB.Driver;
using Serilog;
using Soulgram.Interests.Application;
using Soulgram.Interests.Infrastracture;
using Soulgram.Interests.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .ConfigureLogging((_, logging) => logging.ClearProviders())
    .UseSerilog();

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.MapControllers();

var mongoService = app.Services.GetService(typeof(IMongoClient)) as IMongoClient;
mongoService.SetUpDb(configuration);

app.Run();