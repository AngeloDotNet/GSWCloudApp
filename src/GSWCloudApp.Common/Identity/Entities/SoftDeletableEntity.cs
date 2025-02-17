using GSWCloudApp.Common.Identity.Entities.Interfaces;

namespace GSWCloudApp.Common.Identity.Entities;

/// <summary>
/// Represents an entity that supports soft deletion and tracks creation and modification details.
/// </summary>
/// <typeparam name="TKey">The type of the identifier.</typeparam>
public abstract class SoftDeletableEntity<TKey> : TrackableEntity<TKey>, ISoftDeletableEntity where TKey : IEquatable<TKey>
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
