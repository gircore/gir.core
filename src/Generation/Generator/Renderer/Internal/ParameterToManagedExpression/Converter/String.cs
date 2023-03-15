namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class String : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.String>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = Model.Parameter.GetConvertedName(parameter);
        return $"var {variableName} = {Model.Parameter.GetName(parameter)}.ConvertToString();";
    }
}
