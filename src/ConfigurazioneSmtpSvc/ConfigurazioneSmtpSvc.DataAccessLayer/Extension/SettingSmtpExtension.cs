using ConfigurazioneSmtpSvc.DataAccessLayer.Entities;
using GSWCloudApp.Common.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurazioneSmtpSvc.DataAccessLayer.Extension;

public class SettingSmtpExtension : BaseEntityConfiguration<SettingSmtp>
{
    public override void Configure(EntityTypeBuilder<SettingSmtp> builder)
    {
        builder.ToTable("SettingSmtp");

        builder.Property(p => p.Security).HasConversion<string>().HasColumnName("Security");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}