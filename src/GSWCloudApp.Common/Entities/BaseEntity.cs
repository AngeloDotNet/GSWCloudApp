using GSWCloudApp.Common.Entities.Interfaces;

namespace GSWCloudApp.Common.Entities;

public abstract class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}