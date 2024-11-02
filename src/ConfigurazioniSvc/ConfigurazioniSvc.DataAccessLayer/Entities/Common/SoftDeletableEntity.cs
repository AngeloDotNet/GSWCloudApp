using ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common;

public abstract class SoftDeletableEntity<TKey> : TrackableEntity<TKey>, ISoftDeletableEntity where TKey : IEquatable<TKey>
{
    public Guid? DeletedByUserId { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}
