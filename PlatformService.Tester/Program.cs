using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using PlatformService.Data;
using PlatformService.Models;

Console.WriteLine("Test cosmos sdk v3!");

var endpoint = "<uri>";
var key = "<key>";

CosmosClientBuilder cosmosClientBuilder = new CosmosClientBuilder(accountEndpoint: endpoint, authKeyOrResourceToken: key)
    .WithConsistencyLevel(ConsistencyLevel.Session)
    .WithApplicationRegion(Regions.SwitzerlandNorth);

CosmosClient client = cosmosClientBuilder.Build();

var id = "c5151d13-a2ae-4582-8db9-8ee8655c5fe5";
var database = client.GetDatabase("astraeus-dev");
var container = client.GetContainer("astraeus-dev", "platforms");

Platform? p = null;

// repo
var repo = new PlatformRepository(client, "astraeus-dev", "platforms");

// read something direct
//ItemResponse<Platform> response = await container.ReadItemAsync<Platform>(id, new PartitionKey(id));
//p = response;

// red from repo
p = await repo.GetItemByIdAsync(id);

Console.WriteLine($"Platform {p.Name} ({p.Id})");