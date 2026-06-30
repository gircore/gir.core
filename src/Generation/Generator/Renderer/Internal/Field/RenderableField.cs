using System;
using System.Text;

namespace Generator.Renderer.Internal.Field;

public record RenderableField(string Name, string TypeName, RenderableField.ArrayDefinition? Array)
{
    public record ArrayDefinition(int? FixedSize, int Dimensions);

    public string GetTypeName()
    {
        if (Array is null)
            return TypeName;

        return GetArrayTypeName();
    }

    public string GetArrayTypeName()
    {
        if (Array is null)
            throw new Exception("Field is not an Array");

        var stringArraySigns = new StringBuilder();

        for (var i = 0; i < Array.Dimensions; i++)
            stringArraySigns.Append("[]");

        var arraySuffix = stringArraySigns.ToString();

        return TypeName + arraySuffix;
    }

    public string GetInlineArrayTypeName()
    {
        return Name + "InlineArray";
    }
}
