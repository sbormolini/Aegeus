using Microsoft.Azure.Cosmos;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        string CollectionName { get; }

        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        string GenerateId(T entity);
        Task<T> GetByIdAsync(string id);
        PartitionKey ResolvePartitionKey(string entityId);
        Task UpdateAsync(T entity);
    }
}