using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanCRM.Domain.Entities.CrmItems;

namespace CleanCRM.Infrastructure.Data.Configurations.CrmItems;

public class CrmItemConfiguration : IEntityTypeConfiguration<CrmItem>
{
    public void Configure(EntityTypeBuilder<CrmItem> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
