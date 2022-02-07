using Microsoft.EntityFrameworkCore;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// add cosmos
var accountEndpoint = builder.Configuration.GetValue<string>("CosmosDb:Account");
var accountKey = builder.Configuration.GetValue<string>("CosmosDb:Key");
var dbName = builder.Configuration.GetValue<string>("CosmosDb:DatabaseName");
builder.Services.AddDbContext<AppDbContext>(x => x.UseCosmos(accountEndpoint, accountKey, dbName));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

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
