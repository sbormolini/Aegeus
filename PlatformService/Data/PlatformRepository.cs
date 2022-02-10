using Microsoft.Azure.Cosmos;
using PlatformService.Exceptions;
using PlatformService.Models;
using System.Net;

namespace PlatformService.Data
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly Container _container;
        private readonly CosmosClient _client;

        //public abstract string CollectionName { get; }
        //public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
        //public virtual PartitionKey ResolvePartitionKey(string entityId) => null;

        public PlatformRepository(CosmosClient client)
        {
            _client = client;
        }

        public PlatformRepository(CosmosClient client, string databaseName, string containerName)
        {
            _client = client;
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task<Platform> GetItemByIdAsync(string id)
        {
            try
            {
                var result = await QueryItemAsync($"SELECT * FROM c WHERE c.id = '{id}'");
                return result.FirstOrDefault();
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException();

                throw;
            }
        }

        public async Task<IEnumerable<Platform>> QueryItemAsync(string query)
        {
            try
            {
                QueryDefinition queryDefinition = new QueryDefinition(query);
                FeedIterator<Platform> queryResultSetIterator = _container.GetItemQueryIterator<Platform>(queryDefinition);

                List<Platform> platforms = new();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Platform> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Platform paltform in currentResultSet)
                    {
                        platforms.Add(paltform);
                        //Console.WriteLine("\tRead {0}\n", paltform);
                    }
                }

                return platforms;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException();

                throw;
            }
        }

        public async Task AddItemsToContainerAsync(Platform platform)
        {
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<Platform> platformResponse = await _container.ReadItemAsync<Platform>(platform.Id, new PartitionKey(platform.Id));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing
                ItemResponse<Platform> platformResponse = await _container.CreateItemAsync<Platform>(platform, new PartitionKey(platform.Id));
            }
        }

        public async Task UpdateItemAsync(Platform platform)
        {
            try
            {
                await _container.ReplaceItemAsync(platform, platform.Id);
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException();

                throw;
            }
        }

        public async Task DeleteItemAsync(Platform platform)
        {
            try
            {
                await _container.DeleteItemAsync<Platform>(platform.Id, new PartitionKey(platform.Id));
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                    throw new EntityNotFoundException();

                throw;
            }
        }
    }
}
