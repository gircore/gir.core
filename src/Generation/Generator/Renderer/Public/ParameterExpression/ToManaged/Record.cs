using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToManaged;

internal class Record : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Record>();

    public string GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: record with direction != in not yet supported");

        if (!parameter.IsPointer)
            throw new NotImplementedException($"Unpointed record parameter {parameter.Name} ({parameter.AnyType}) can not yet be converted to managed");

        var record = (GirModel.Record) parameter.AnyType.AsT0;
        var ownedHandle = parameter.Transfer == GirModel.Transfer.Full;

        variableName = Parameter.GetConvertedName(parameter);

        var handleClass = ownedHandle
            ? Model.Record.GetFullyQualifiedInternalOwnedHandle(record)
            : Model.Record.GetFullyQualifiedInternalUnownedHandle(record);

        return $"var {variableName} = new {Model.Record.GetFullyQualifiedPublicClassName(record)}(new {handleClass}({Parameter.GetName(parameter)}));";
    }
}
