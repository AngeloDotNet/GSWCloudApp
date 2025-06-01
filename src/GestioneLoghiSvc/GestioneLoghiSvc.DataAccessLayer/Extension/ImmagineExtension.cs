using GestioneLoghiSvc.DataAccessLayer.Entities;
using GSWCloudApp.Common.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneLoghiSvc.DataAccessLayer.Extension;

public class ImmagineExtension : BaseEntityConfiguration<Immagine>
{
    public override void Configure(EntityTypeBuilder<Immagine> builder)
    {
        builder.ToTable("Immagini");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}