using System;

namespace Generator3.Converter.Parameter.ToManaged;

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

        variableName = parameter.GetConvertedName();

        var handleClass = ownedHandle
            ? record.GetFullyQualifiedInternalOwnedHandle()
            : record.GetFullyQualifiedInternalUnownedHandle();

        return $"var {variableName} = new {record.GetFullyQualifiedPublicClassName()}(new {handleClass}({parameter.GetPublicName()}));";
    }
}
