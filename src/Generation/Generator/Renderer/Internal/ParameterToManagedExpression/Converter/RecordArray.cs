using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class RecordArray : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;
        var record = (GirModel.Record) arrayType.AnyType.AsT0;
        variableName = Parameter.GetConvertedName(parameter);

        if (arrayType.IsPointer)
        {
            return $"var {variableName} = {Parameter.GetName(parameter)}.Select(x => new {Model.Record.GetFullyQualifiedPublicClassName(record)}(x)).ToArray();";
        }
        else
        {
            return $"var {variableName} = ({Model.Record.GetFullyQualifiedPublicClassName(record)}[]){Parameter.GetName(parameter)}.Select(x => new {Model.Record.GetFullyQualifiedPublicClassName(record)}({Model.Record.GetFullyQualifiedInternalManagedHandleCreateMethod(record)}(x))).ToArray();";
        }
    }
}
