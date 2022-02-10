using Microsoft.Azure.Cosmos;

namespace PlatformService.Data;

public class CosmosDBClient
{
    private readonly string _databaseName;
    private readonly string _containerName;

    private readonly CosmosClient _client;
    private readonly Database _database;
    private readonly Container _container;

    public CosmosDBClient(string databaseName, string containerName, CosmosClient client)
    {
        _databaseName = databaseName;
        _containerName = containerName;
        _client = client;
    }

    //public async Task<ContainerResponse> CreateContainerAsync(
    //    string containerId, 
    //    string partitionKeyPath,
    //    int? throughput,
    //    RequestOptions? requestOptions = null,
    //    CancellationToken cancellationToken = default)
    //{
    //    // Create a new container
    //    var response = await _database.CreateContainerIfNotExistsAsync(containerId, partitionKeyPath, throughput, requestOptions, cancellationToken);
    //    if (response is null)
    //        throw new InvalidOperationException($"Response is null {nameof(response)}");
        
    //    return response;
    //}

    //public async Task AddItemsToContainerAsync()
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task RemoveItemsFromContainerAsync()
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<ItemResponse> QueryItemsAsync<T>(string sqlQueryText)
    //{
    //    QueryDefinition queryDefinition = new (sqlQueryText);
    //    var items = await _container.GetItem
    //}
}
