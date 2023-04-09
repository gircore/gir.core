namespace Generator.Model;

internal static class Parameter
{
    public static string GetName(GirModel.Parameter parameter)
    {
        return parameter.Name.ToCamelCase().EscapeIdentifier();
    }

    public static string GetConvertedName(GirModel.Parameter parameter)
    {
        return GetName(parameter) + "Native";
    }

    public static bool IsGLibError(GirModel.Parameter parameter)
    {
        if (parameter.Direction != GirModel.Direction.Out)
            return false;

        if (!parameter.IsPointer)
            return false;

        if (parameter.AnyTypeOrVarArgs.TryPickT0(out var anyType, out _)
            && anyType.TryPickT0(out var type, out _)
            && type is GirModel.Record record)
        {
            return Record.IsGLibError(record);
        }

        return false;
    }
}
