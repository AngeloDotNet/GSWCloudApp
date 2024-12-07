using GSWCloudApp.Common.Entities.Interfaces;

namespace GSWCloudApp.Common.Entities;

/// <summary>
/// Represents a base entity with a generic identifier.
/// </summary>
/// <typeparam name="TKey">The type of the identifier.</typeparam>
public abstract class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    public TKey Id { get; set; } = default!;
}
