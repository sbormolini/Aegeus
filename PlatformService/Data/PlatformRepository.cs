using Microsoft.Azure.Cosmos;
using PlatformService.Data.CosmosDB;
using PlatformService.Exceptions;
using PlatformService.Models;
using System.Net;

namespace PlatformService.Data;

public class PlatformRepository : CosmosDBRepository<Platform>, IPlatformRepository
{

}
