using PlatformService.Models;

namespace PlatformService.Data;

public interface IPlatformRepository
{
    bool SaveChanges();

    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(string id);
    void CreatePlatform(Platform platform);
}
