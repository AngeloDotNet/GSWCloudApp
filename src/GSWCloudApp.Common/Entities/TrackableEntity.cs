using GSWCloudApp.Common.Entities.Interfaces;

namespace GSWCloudApp.Common.Entities;

public abstract class TrackableEntity<TKey> : BaseEntity<TKey>, ITrackableEntity where TKey : IEquatable<TKey>
{
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedDateTime { get; set; }

    public Guid? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}