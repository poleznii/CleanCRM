using CleanCRM.Domain.Entities.CrmItems;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Infrastructure.Data.Configurations.CrmItems;

internal class CrmItemPropertyValueConfiguration : IEntityTypeConfiguration<CrmItemPropertyValue>
{
    public void Configure(EntityTypeBuilder<CrmItemPropertyValue> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}
