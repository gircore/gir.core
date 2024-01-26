using System;
using GirModel;

namespace Generator.Renderer.Internal.ReturnType;

internal class UntypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyType.AsT0;

        var typeName = returnType switch
        {
            { Transfer: Transfer.Full } => Model.TypedRecord.GetFullyQuallifiedOwnedHandle(type),
            { Transfer: Transfer.None } => Model.TypedRecord.GetFullyQuallifiedUnownedHandle(type),
            _ => throw new Exception($"Unsupported transfer type {returnType.Transfer} for untyped record {type.Name}")
        };

        //Returned SafeHandles are never "null" but "invalid" in case of C NULL.
        return new RenderableReturnType(typeName);
    }
}
