using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        Task AddItemsToContainerAsync(Platform platform);
        Task DeleteItemAsync(Platform platform);
        Task<Platform> GetItemByIdAsync(string id);
        Task<IEnumerable<Platform>> QueryItemAsync(string query);
        Task UpdateItemAsync(Platform platform);
    }
}