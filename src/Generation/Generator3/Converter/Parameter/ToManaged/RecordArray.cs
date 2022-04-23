namespace Generator3.Converter.Parameter.ToManaged;

internal class RecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.Record>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;
        var record = (GirModel.Record) arrayType.AnyType.AsT0;
        variableName = parameter.GetConvertedName();

        if (arrayType.IsPointer)
        {
            return $"var {variableName} = {parameter.GetPublicName()}.Select(x => new {record.GetFullyQualifiedPublicClassName()}(x)).ToArray();";
        }
        else
        {
            return $"var {variableName} = ({record.GetFullyQualifiedPublicClassName()}[]){parameter.GetPublicName()}.Select(x => new {record.GetFullyQualifiedPublicClassName()}({record.GetFullyQualifiedInternalManagedHandleCreateMethod()}(x))).ToArray();";
        }
    }
}
