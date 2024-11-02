using ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common;

public abstract class TrackableEntity<TKey> : BaseEntity<TKey>, ITrackableEntity where TKey : IEquatable<TKey>
{
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedDateTime { get; set; }

    public Guid? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}