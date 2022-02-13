using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Services;

public class PlaftomService
{
    private readonly IPlatformRepository _platformRepository;

    public PlaftomService(IPlatformRepository platformRepository) => _platformRepository = platformRepository;

    public async Task<(bool, Exception?)> CreatePlatform(Platform platform)
    {
        try
        {
            await _platformRepository.AddItemsToContainerAsync(platform);
            return (true, null);
        }
        catch (Exception ex)
        {
            return (false, ex);
        }
    }

    public async Task<(Platform?, Exception)> GetById(Guid id)
    {
        try
        {
            var platform = await _platformRepository.GetItemByIdAsync(id.ToString());
            return (platform, null);
        }
        catch (Exception ex)
        {
            return (null, ex);
        }
    }

    public List<Platform> GetAll() => _platformRepository.Values.ToList();

    public bool Update(Platform customer)
    {
        var existingCustomer = GetById(customer.Id);
        if (existingCustomer is null)
            return false;

        _customers[customer.Id] = customer;
        return true;
    }

    public bool Delete(Guid id)
    {
        var existingCustomer = GetById(id);
        if (existingCustomer is null)
            return false;

        _customers.Remove(id);
        return true;
    }
}