using Microsoft.Azure.Cosmos;

namespace PlatformService.Data;

public class CosmosDBRepository<T> : ICosmosDBRepository<T> where T : class
{
    private readonly Container _container;

    public CosmosDBRepository(Container container)
    {
        _container = container ?? throw new ArgumentNullException(nameof(container));
    }

    public async Task<T> CreateItemAsync<T>(T item, string partitionKey) where T : class
    {
        return await _container.CreateItemAsync(item, new PartitionKey(partitionKey));
    }

    public async Task<T> GetItemAsync<T>(string id, string partitionKey) where T : class
    {
        try
        {
            ItemResponse<T> cosmosItemResponse = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            ItemRequestOptions itemRequestOptions = new()
            {
                IfMatchEtag = cosmosItemResponse.ETag
            };
   
            return (T)(dynamic)cosmosItemResponse;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
