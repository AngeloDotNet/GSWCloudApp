using GestioneDocumentiSvc.DataAccessLayer.Entities;
using GSWCloudApp.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneDocumentiSvc.DataAccessLayer.Extension;

public class DocumentoExtension : BaseEntityConfiguration<Documento>
{
    public override void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable("Documenti");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}