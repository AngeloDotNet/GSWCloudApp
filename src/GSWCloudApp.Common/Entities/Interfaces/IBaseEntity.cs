namespace GSWCloudApp.Common.Entities.Interfaces;

public interface IBaseEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; set; }
}
