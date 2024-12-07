using GSWCloudApp.Common.Entities.Interfaces;

namespace GSWCloudApp.Common.Entities;

/// <summary>
/// Represents an entity that tracks creation and modification details.
/// </summary>
/// <typeparam name="TKey">The type of the identifier.</typeparam>
public abstract class TrackableEntity<TKey> : BaseEntity<TKey>, ITrackableEntity where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the ID of the user who created the entity.
    /// </summary>
    public Guid CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedDateTime { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who last modified the entity.
    /// Nullable to indicate that the entity may not have been modified.
    /// </summary>
    public Guid? ModifiedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// Nullable to indicate that the entity may not have been modified.
    /// </summary>
    public DateTime? ModifiedDateTime { get; set; }
}
