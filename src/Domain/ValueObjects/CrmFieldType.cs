using CleanCRM.Domain.Common;

namespace CleanCRM.Domain.ValueObjects;

public class CrmFieldType : ValueObject
{
    static CrmFieldType()
    {
    }

    private CrmFieldType()
    {
    }

    private CrmFieldType(string code)
    {
        Code = code;
    }

    public static CrmFieldType From(string code)
    {
        var colour = new CrmFieldType { Code = code };

        return colour;
    }


    public string Code { get; private set; } = "string";
    public static CrmFieldType String => new("string");
    public static CrmFieldType Integer => new("integer");
    public static CrmFieldType File => new("file");
    public static CrmFieldType DateTime => new("datetime");


    public static implicit operator string(CrmFieldType colour)
    {
        return colour.ToString();
    }

    public static explicit operator CrmFieldType(string code)
    {
        return From(code);
    }

    public override string ToString()
    {
        return Code;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
    }
}
