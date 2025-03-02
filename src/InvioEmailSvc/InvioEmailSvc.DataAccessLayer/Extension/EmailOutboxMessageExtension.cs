using GSWCloudApp.Common.Identity.Entities;
using InvioEmailSvc.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvioEmailSvc.DataAccessLayer.Extension;

public class EmailOutboxMessageExtension : BaseEntityConfiguration<EmailOutboxMessage>
{
    public override void Configure(EntityTypeBuilder<EmailOutboxMessage> builder)
    {
        builder.ToTable("InvioOutboxEmail");

        builder.HasQueryFilter(builder => builder.DeletedDateTime == null);
    }
}