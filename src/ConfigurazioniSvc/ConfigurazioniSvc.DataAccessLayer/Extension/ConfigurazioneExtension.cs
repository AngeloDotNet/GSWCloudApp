using ConfigurazioniSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurazioniSvc.DataAccessLayer.Extension;

public class ConfigurazioneExtension : IEntityTypeConfiguration<Configurazione>
{
    public void Configure(EntityTypeBuilder<Configurazione> builder)
    {
        builder.ToTable("Configurazione");
        builder.HasKey(builder => builder.Id);

        builder.Property(builder => builder.Id)
            .ValueGeneratedOnAdd();

        builder.Property(builder => builder.Obbligatorio)
            .HasDefaultValue(false);

        builder.Property(builder => builder.Scope)
            .HasConversion<string>()
            .HasColumnName("Scope");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}
