using Microsoft.Azure.Cosmos;

namespace PlatformService.Data.CosmosDB
{
    public interface ICosmosDBContainer
    {
        Container Container { get; }
    }
}