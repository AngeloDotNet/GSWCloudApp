namespace GSWCloudApp.Common.Entities.Interfaces;

/// <summary>
/// Represents an entity that supports soft deletion.
/// </summary>
public interface ISoftDeletableEntity
{
    /// <summary>
    /// Gets or sets the identifier of the user who deleted the entity.
    /// </summary>
    public Guid? DeletedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was deleted.
    /// </summary>
    public DateTime? DeletedDateTime { get; set; }
}
