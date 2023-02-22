using CleanCRM.Domain.Entities.CrmTypes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CleanCRM.Infrastructure.Data.Configurations.CrmTypes;

public class CrmTypeConfigurationn : IEntityTypeConfiguration<CrmType>
{
    public void Configure(EntityTypeBuilder<CrmType> builder)
    {
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Type)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
