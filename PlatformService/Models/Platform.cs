﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PlatformService.Models;

public class Platform
{
    [Key]
    [Required]
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [JsonProperty(PropertyName = "name")]
    public string? Name { get; set; }

    [Required]
    [JsonProperty(PropertyName = "publisher")]
    public string? Publisher { get; set; }

    [Required]
    [JsonProperty(PropertyName = "cost")]
    public string? Cost { get; set; }
}
