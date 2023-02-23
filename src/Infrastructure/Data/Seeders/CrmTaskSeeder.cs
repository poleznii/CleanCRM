using CleanCRM.Domain.Entities.CrmTypes;
using CleanCRM.Domain.Entities.CrmItems;
using CleanCRM.Domain.ValueObjects;

namespace CleanCRM.Infrastructure.Data.Seeders;

internal class CrmTaskSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var typeEntity = CreateType();

        context.CrmTypes.Add(typeEntity);

        var tasks = new List<CrmItem>();

        for (int i = 1; i < 21; i++)
        {
            tasks.Add(CreateTask(typeEntity, $"Task {i}", $"Task Description {i}", DateTime.UtcNow.AddDays(i).ToString("s")));
        }

        context.CrmItems.AddRange(tasks);
        await context.SaveChangesAsync();
    }

    private static CrmType CreateType()
    {
        var typeEntity = new CrmType()
        {
            Id = "task"
        };
        typeEntity.Fields.Add(new CrmTypeField()
        {
            FieldType = CrmFieldType.String,
            Name = "Name"
        });
        typeEntity.Fields.Add(new CrmTypeField()
        {
            FieldType = CrmFieldType.String,
            Name = "Description"
        });
        typeEntity.Fields.Add(new CrmTypeField()
        {
            FieldType = CrmFieldType.DateTime,
            Name = "Deadline"
        });

        return typeEntity;
    }

    private static CrmItem CreateTask(CrmType type, string name, string description, string deadline)
    {
        var result = new CrmItem()
        {
            Type = type
        };

        var nameField = TryCreatePropertyValue(type, "Name", name);
        if (nameField != null)
        {
            result.Fields.Add(nameField);
        }

        var descriptionField = TryCreatePropertyValue(type, "Description", description);
        if (descriptionField != null)
        {
            result.Fields.Add(descriptionField);
        }

        var deadlineField = TryCreatePropertyValue(type, "Deadline", deadline);
        if (deadlineField != null)
        {
            result.Fields.Add(deadlineField);
        }

        return result;
    }

    private static CrmItemField? TryCreatePropertyValue(CrmType type, string fieldName, string fieldValue)
    {
        var fieldEntity = type.Fields.FirstOrDefault(x => x.Name.Equals(fieldName));
        if (fieldEntity == null)
        {
            return null;
        }
        var property = new CrmItemField()
        {
            Field = fieldEntity
        };
        property.Values.Add(new CrmItemPropertyValue()
        {
            Raw = fieldValue
        });
        return property;
    }
}
