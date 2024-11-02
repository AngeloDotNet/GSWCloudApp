namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

public interface ISoftDeletableEntity
{
    public Guid? DeletedByUserId { get; set; }
    public DateTime? DeletedDateTime { get; set; }
}