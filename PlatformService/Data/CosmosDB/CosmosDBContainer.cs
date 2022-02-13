using Microsoft.Azure.Cosmos;

namespace PlatformService.Data.CosmosDB;

public class CosmosDBContainer : ICosmosDBContainer
{
    public Container Container { get; }

    public CosmosDBContainer(CosmosClient cosmosClient, string databaseName, string containerName) =>
        Container = cosmosClient.GetContainer(databaseName, containerName);

}
