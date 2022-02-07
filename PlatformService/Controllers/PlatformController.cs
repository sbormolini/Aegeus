using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Models;

namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly AppDbContext _appDbContext;

    public PlatformController(
        AppDbContext appDbContext,
        IConfiguration configuration,
        ILogger<PlatformController> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    [HttpGet("GetAllPlatforms")]
    public async Task<ActionResult<IList<Platform>>> GetAllPlatforms()
    {
        try
        {
            var platforms = await _appDbContext.Platforms.ToListAsync();
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

    [HttpPost]
    public async Task<ActionResult> CreatePlatform(Platform platform)
    {
        try
        {
            await _appDbContext.Platforms.AddAsync(platform);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return StatusCode(500, "Some error occured while adding data");
        }
    }
}
