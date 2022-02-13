using Microsoft.Azure.Cosmos;

namespace PlatformService.Data.CosmosDB;

public class CosmosDBContainerFactory : ICosmosDBContainerFactory
{
    /// <summary>
    ///     Azure Cosmos DB Client
    /// </summary>
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly List<ContainerInfo> _containers;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="cosmosClient"></param>
    /// <param name="databaseName"></param>
    /// <param name="containers"></param>
    public CosmosDBContainerFactory(CosmosClient cosmosClient, string databaseName, List<ContainerInfo> containers)
    {
        _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
        _containers = containers ?? throw new ArgumentNullException(nameof(containers));
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
    }

    public ICosmosDBContainer GetContainer(string containerName)
    {
        if (_containers.Where(x => x.Name == containerName) is null)
            throw new ArgumentException($"Unable to find container: {containerName}");
        
        return new CosmosDBContainer(_cosmosClient, _databaseName, containerName);
    }

    public async Task EnsureDatabaseSetupAsync()
    {
        DatabaseResponse database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        foreach (ContainerInfo container in _containers)
            await database.Database.CreateContainerIfNotExistsAsync(container.Name, $"{container.PartitionKey}");
    }
}
