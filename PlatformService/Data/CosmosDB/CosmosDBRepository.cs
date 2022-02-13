using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using PlatformService.Data.Base;
using PlatformService.Data.Base.Entities;

namespace PlatformService.Data.CosmosDB;

public abstract class CosmosDBRepository<T> : IRepository<T>, IContainerContext<T> where T : BaseEntity
{

    public abstract string ContainerName { get; }

    public abstract string GenerateId(T entity);

    public abstract PartitionKey ResolvePartitionKey(string entityId);

    public virtual string GenerateAuditId(Audit entity) => 
        $"{entity.EntityId}:{Guid.NewGuid()}";

    public virtual PartitionKey ResolveAuditPartitionKey(string entityId) => 
        new($"{entityId.Split(':')[0]}:{entityId.Split(':')[1]}");


    private readonly ICosmosDBContainerFactory _cosmosDBContainerFactory;


    private readonly Container _container;


    private readonly Container _auditContainer;

    public CosmosDBRepository(ICosmosDBContainerFactory cosmosDbContainerFactory)
    {
        _cosmosDBContainerFactory = cosmosDbContainerFactory ?? throw new ArgumentNullException(nameof(ICosmosDBContainerFactory));
        _container = _cosmosDBContainerFactory.GetContainer(ContainerName).Container;
        _auditContainer = _cosmosDBContainerFactory.GetContainer("Audit").Container;
    }

    public async Task AddItemAsync(T item)
    {
        item.Id = GenerateId(item);
        await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id));
    }

    public async Task<T> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
    {
        FeedIterator<T> resultSetIterator = _container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
        List<T> results = new();
        while (resultSetIterator.HasMoreResults)
        {
            FeedResponse<T> response = await resultSetIterator.ReadNextAsync();

            results.AddRange(response.ToList());
        }

        return results;
    }

    public async Task UpdateItemAsync(string id, T item)
    {
        // Audit
        await Audit(item);
        // Update
        await _container.UpsertItemAsync<T>(item, ResolvePartitionKey(id));
    }

    private async Task Audit(T item)
    {
        Audit auditItem = new(item.GetType().Name, item.Id, JsonConvert.SerializeObject(item));
        auditItem.Id = GenerateAuditId(auditItem);
        await _auditContainer.CreateItemAsync(auditItem, ResolveAuditPartitionKey(auditItem.Id));
    }
}
