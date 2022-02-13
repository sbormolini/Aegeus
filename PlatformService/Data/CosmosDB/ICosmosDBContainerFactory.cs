
namespace PlatformService.Data.CosmosDB
{
    public interface ICosmosDBContainerFactory
    {
        Task EnsureDatabaseSetupAsync();
        ICosmosDBContainer GetContainer(string containerName);
    }
}