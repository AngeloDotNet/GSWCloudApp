namespace ConfigurazioniSvc.DataAccessLayer.Entities.Common.Interfaces;

public interface ITrackableEntity
{
    public Guid CreatedByUserId { get; set; }
    public DateTime CreatedDateTime { get; set; }

    public Guid? ModifiedByUserId { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
}
