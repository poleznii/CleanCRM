using CleanCRM.Domain.Entities.CrmItems;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Infrastructure.Data.Configurations.CrmItems;

internal class CrmItemFieldConfiguration : IEntityTypeConfiguration<CrmItemField>
{
    public void Configure(EntityTypeBuilder<CrmItemField> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}

