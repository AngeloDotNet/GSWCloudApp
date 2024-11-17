namespace GSWCloudApp.Common.Entities.Interfaces;

public interface ISoftDeletableEntity
{
    public Guid? DeletedByUserId { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}