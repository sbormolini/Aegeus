using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using PlatformService.Data;
using PlatformService.Models;

Console.WriteLine("Test cosmos sdk v3!");

var endpoint = "<uri>";
var key = "<ke>";

CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(accountEndpoint: endpoint, authKeyOrResourceToken: key);
    //.WithConsistencyLevel(ConsistencyLevel.Session)
    //.WithApplicationRegion(Regions.SwitzerlandNorth);

CosmosClient client = cosmosClientBuilder.Build();

var id = "c5151d13-a2ae-4582-8db9-8ee8655c5fe5";
var databaseName = "astraeus-dev";
var containerName = "platforms";
//var database = client.GetDatabase("astraeus-dev");

Platform? p = null;

// read direct
var container = client.GetContainer(databaseName, containerName);
ItemResponse<Platform> response = await container.ReadItemAsync<Platform>(id, new PartitionKey(id));
p = response;
Console.WriteLine($"Platform from client {p.Name} ({p.Id})");

// read from repo
var repository = new PlatformRepository(client, databaseName, containerName);
p = await repository.GetItemByIdAsync(id);
Console.WriteLine($"Platform from repo {p.Name} ({p.Id})");