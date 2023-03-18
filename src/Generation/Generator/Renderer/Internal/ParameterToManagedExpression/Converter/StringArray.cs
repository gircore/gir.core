namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class StringArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        if (parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            variableName = Model.Parameter.GetConvertedName(parameter);
            return $"var {variableName} = GLib.Internal.StringHelper.ToStringArrayUtf8({Model.Parameter.GetName(parameter)});";
        }
        else
        {
            //We don't need any conversion for string[]
            variableName = Model.Parameter.GetName(parameter);
            return null;
        }
    }
}
