using System.Linq;

namespace Generator.Model;

internal static partial class Function
{
    public static string GetName(GirModel.Function function)
    {
        if (function.Shadows is null)
            return function.Name.ToPascalCase().EscapeIdentifier();

        if (function.Parameters.Count() != function.Shadows.Parameters.Count())
            return function.Shadows.Name.ToPascalCase().EscapeIdentifier();

        if (function.Parameters.Select(x => x.AnyTypeOrVarArgs).Except(function.Shadows.Parameters.Select(x => x.AnyTypeOrVarArgs)).Any())
            return function.Shadows.Name.ToPascalCase().EscapeIdentifier();

        return function.Name.ToPascalCase().EscapeIdentifier();
    }
}
