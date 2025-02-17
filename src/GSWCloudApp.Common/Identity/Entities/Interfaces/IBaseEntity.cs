namespace GSWCloudApp.Common.Identity.Entities.Interfaces;

/// <summary>
/// Represents a base entity with a generic identifier.
/// </summary>
/// <typeparam name="TKey">The type of the identifier.</typeparam>
public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the identifier of the entity.
    /// </summary>
    TKey Id { get; set; }
}
