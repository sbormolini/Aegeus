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

        public PlatformRepository(CosmosClient client)
        {
            _client = client;
            _container = client.GetContainer("astraeus-dev", "platforms");
        }

        public PlatformRepository(CosmosClient client, string databaseName, string containerName)
        {
            _client = client;
            _container = client.GetContainer(databaseName, containerName);
        }

        public async Task<Platform?> GetItemByIdAsync(string id)
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
                        platforms.Add(paltform);
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
                ItemResponse<Platform> platformResponse = await _container.ReadItemAsync<Platform>(
                    platform.Id.ToString(), 
                    new PartitionKey(platform.Id.ToString()));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                ItemResponse<Platform> platformResponse = await _container.CreateItemAsync(
                    platform, 
                    new PartitionKey(platform.Id.ToString()));
            }
        }

        public async Task UpdateItemAsync(Platform platform)
        {
            try
            {
                await _container.ReplaceItemAsync(
                    platform, 
                    platform.Id.ToString());
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
                await _container.DeleteItemAsync<Platform>(
                    platform.Id.ToString(), 
                    new PartitionKey(platform.Id.ToString()));
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
