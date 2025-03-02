using GSWCloudApp.Common.Identity.Entities;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvioEmailSvc.DataAccessLayer.Extension;

public class EmailMessageExtension : BaseEntityConfiguration<EmailMessage>
{
    public override void Configure(EntityTypeBuilder<EmailMessage> builder)
    {
        builder.ToTable("InvioEmail");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}