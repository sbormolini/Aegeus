using Newtonsoft.Json;

namespace PlatformService.Data.Base.Entities;

public abstract class BaseEntity
{
    [JsonProperty(PropertyName = "id")]
    public virtual string Id { get; set; }
}