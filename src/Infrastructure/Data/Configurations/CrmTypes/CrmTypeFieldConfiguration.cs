using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CleanCRM.Domain.Entities.CrmTypes;

namespace CleanCRM.Infrastructure.Data.Configurations.CrmTypes;

public class CrmTypeFieldConfiguration : IEntityTypeConfiguration<CrmTypeField>
{
    public void Configure(EntityTypeBuilder<CrmTypeField> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder
            .OwnsOne(b => b.FieldType);

    }
}
