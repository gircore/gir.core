using System.Linq;

namespace Generator.Model;

internal static class Constructor
{
    public static string GetName(GirModel.Constructor constructor)
    {
        if (constructor.Shadows is null)
            return constructor.Name.ToPascalCase().EscapeIdentifier();

        if (constructor.Parameters.Count() != constructor.Shadows.Parameters.Count())
            return constructor.Shadows.Name.ToPascalCase().EscapeIdentifier();

        if (constructor.Parameters.Select(x => x.AnyTypeOrVarArgs).Except(constructor.Shadows.Parameters.Select(x => x.AnyTypeOrVarArgs)).Any())
            return constructor.Shadows.Name.ToPascalCase().EscapeIdentifier();

        return constructor.Name.ToPascalCase().EscapeIdentifier();
    }
}
