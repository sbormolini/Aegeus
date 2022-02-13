using Microsoft.Azure.Cosmos;
using PlatformService.Data.Base.Entities;

namespace PlatformService.Data.CosmosDB;

public interface IContainerContext<T> where T : BaseEntity
{
    string ContainerName { get; }
    string GenerateId(T entity);
    PartitionKey ResolvePartitionKey(string entityId);
}
