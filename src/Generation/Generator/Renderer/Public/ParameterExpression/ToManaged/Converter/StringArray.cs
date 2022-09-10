using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToManaged;

internal class StringArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;
        if (parameter.Transfer == GirModel.Transfer.None && arrayType.Length == null)
        {
            variableName = Parameter.GetConvertedName(parameter);
            return $"var {variableName} = GLib.Internal.StringHelper.ToStringArrayUtf8({Parameter.GetName(parameter)});";
        }
        else
        {
            //We don't need any conversion for string[]
            variableName = Parameter.GetName(parameter);
            return null;
        }
    }
}
