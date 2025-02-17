using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GSWCloudApp.Common.Identity.Entities;

/// <summary>
/// Base configuration class for entities inheriting from BaseEntity with a Guid identifier.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity<Guid>
{
    /// <summary>
    /// Configures the entity of type T.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
