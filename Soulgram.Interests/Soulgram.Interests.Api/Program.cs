using MongoDB.Driver;
using Serilog;
using Soulgram.Interests.Application;
using Soulgram.Interests.Infrastructure;
using Soulgram.Interests.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Host
    .ConfigureLogging((_, logging) => logging.ClearProviders())
    .UseSerilog();

var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);
builder.Services.AddInfrastructure(configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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
(mongoService ?? throw new InvalidOperationException()).SetUpDb(configuration);

app.Run();