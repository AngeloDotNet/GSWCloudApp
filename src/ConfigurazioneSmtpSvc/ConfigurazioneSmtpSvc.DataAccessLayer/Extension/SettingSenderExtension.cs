using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using GSWCloudApp.Common.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurazioneSmtpSvc.DataAccessLayer.Extension;

public class SettingSenderExtension : BaseEntityConfiguration<SettingSender>
{
    public override void Configure(EntityTypeBuilder<SettingSender> builder)
    {
        builder.ToTable("SettingSender");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}