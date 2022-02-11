using Microsoft.Azure.Cosmos;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// add cosmos
var accountEndpoint = builder.Configuration.GetValue<string>("CosmosDB:AccountEndpoint");
var databaseName = builder.Configuration.GetValue<string>("CosmosDB:DatabaseName");
var containerNmae = builder.Configuration.GetValue<string>("CosmosDB:ContainerName");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<CosmosClient>(serviceProvider =>
{
    IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    CosmosClientOptions cosmosClientOptions = new()
    {
        HttpClientFactory = httpClientFactory.CreateClient,
        ConnectionMode = ConnectionMode.Gateway
    };

    return new CosmosClient(accountEndpoint, cosmosClientOptions);
});
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
