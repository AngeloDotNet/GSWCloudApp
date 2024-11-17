using GSWCloudApp.Common.Entities.Interfaces;

namespace GSWCloudApp.Common.Entities;

public abstract class SoftDeletableEntity<TKey> : TrackableEntity<TKey>, ISoftDeletableEntity where TKey : IEquatable<TKey>
{
    public Guid? DeletedByUserId { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}
