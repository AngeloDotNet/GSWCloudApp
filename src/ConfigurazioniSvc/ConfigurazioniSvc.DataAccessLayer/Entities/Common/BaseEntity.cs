using ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common;

public abstract class BaseEntity<TKey> : IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}