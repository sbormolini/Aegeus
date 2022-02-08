using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using PlatformService.Models;

Console.WriteLine("Test cosmos sdk v3!");


CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(accountEndpoint: endpoint, authKeyOrResourceToken: key)
    .WithConsistencyLevel(ConsistencyLevel.Session)
    .WithApplicationRegion(Regions.SwitzerlandNorth);

CosmosClient client = cosmosClientBuilder.Build();

var database = client.GetDatabase("astraeus-dev");
var container = client.GetContainer("astraeus-dev", "platforms");

// read something
var id = "c5151d13-a2ae-4582-8db9-8ee8655c5fe5";
ItemResponse<Platform> response = await container.ReadItemAsync<Platform>(id, new PartitionKey(id));
Platform p = response;

Console.WriteLine($"Platform {p.Name} ({p.Id})");