namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}
