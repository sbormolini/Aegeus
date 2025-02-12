﻿namespace PlatformService.Data.Base.Entities;

public class Audit : BaseEntity
{
    /// <summary>
    ///     Type of the entity, e.g., ToDoItem
    /// </summary>
    public string EntityType { get; set; }

    /// <summary>
    ///     Entity Id.
    ///     Use this as the Partition Key, so that all the auditing records for the same entity are stored in the same logical partition.
    /// </summary>
    public string EntityId { get; set; }

    /// <summary>
    ///     Entity itself
    /// </summary>
    public string Entity { get; set; }

    /// <summary>
    ///     Date audit record created
    /// </summary>
    public DateTime DateCreatedUTC { get; set; }

    public Audit(string entityType, string entityId, string entity)
    {
        EntityType = entityType;
        EntityId = entityId;
        Entity = entity;
        DateCreatedUTC = DateTime.UtcNow;
    }

}