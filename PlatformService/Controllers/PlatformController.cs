using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IPlatformRepository _repository;

    public PlatformController(
        IPlatformRepository repository,
        IConfiguration configuration,
        ILogger<PlatformController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Platform>>> GetAll()
    {
        try
        {
            var platforms = await _repository.QueryItemAsync("SELECT * FROM c");
            if (platforms is null)
                return NotFound("No results found");

            return Ok(platforms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Platform>> GetById([FromRoute] Guid id)
    {
        try
        {
            var platform = await _repository.GetItemByIdAsync(id.ToString());
            if (platform is null)
                return NotFound("No results found");

            return Ok(platform);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, $"Some error occured while retrieving data: {ex}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Platform platform)
    {
        try
        {
            await _repository.AddItemsToContainerAsync(platform);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Some error occured while adding data");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Platform updatedPlatform)
    {
        try
        {
            await _repository.UpdateItemAsync(updatedPlatform);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Some error occured while adding data");
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Platform platform)
    {
        try
        {
            await _repository.DeleteItemAsync(platform);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Some error occured while adding data");
        }
    }
}
