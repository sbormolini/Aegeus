namespace PlatformService.Data.Base.Entities;

public class Platform : BaseEntity
{
    public string? Name { get; set; }

    public string? Publisher { get; set; }

    public string? Cost { get; set; }
}