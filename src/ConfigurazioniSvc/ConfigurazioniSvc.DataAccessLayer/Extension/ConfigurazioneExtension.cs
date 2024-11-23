using ConfigurazioniSvc.DataAccessLayer.Entities;
using GSWCloudApp.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurazioniSvc.DataAccessLayer.Extension;

public class ConfigurazioneExtension : BaseEntityConfiguration<Configurazione>
{
    public override void Configure(EntityTypeBuilder<Configurazione> builder)
    {
        builder.ToTable("Configurazione");
        builder.HasIndex(builder => builder.Chiave).IsUnique();

        builder.Property(builder => builder.Obbligatorio).HasDefaultValue(false);
        builder.Property(builder => builder.Scope).HasConversion<string>().HasColumnName("Scope");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}
