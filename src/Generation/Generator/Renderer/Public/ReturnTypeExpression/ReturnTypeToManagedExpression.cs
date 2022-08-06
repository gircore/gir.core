using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class ReturnTypeToManagedExpression
{
    private static readonly List<ReturnTypeExpression.ReturnTypeConverter> Converter = new()
    {
        new ReturnTypeExpression.ToManaged.Pointer(),
        new ReturnTypeExpression.ToManaged.Bitfield(),
        new ReturnTypeExpression.ToManaged.Enumeration(),
        new ReturnTypeExpression.ToManaged.PrimitiveValueType(),
        new ReturnTypeExpression.ToManaged.PrimitiveValueTypeArray(),
        new ReturnTypeExpression.ToManaged.Utf8String(),
        new ReturnTypeExpression.ToManaged.StringArray(),
        new ReturnTypeExpression.ToManaged.PlatformString(),
        new ReturnTypeExpression.ToManaged.Class(),
        new ReturnTypeExpression.ToManaged.Interface(),
        new ReturnTypeExpression.ToManaged.Record(),
    };

    public static string Render(GirModel.ReturnType from, string fromVariableName)
    {
        foreach (var converter in Converter)
            if (converter.Supports(from.AnyType))
                return converter.GetString(from, fromVariableName);

        throw new System.NotImplementedException($"Missing converter to convert from internal return type {from} to public.");
    }
}
