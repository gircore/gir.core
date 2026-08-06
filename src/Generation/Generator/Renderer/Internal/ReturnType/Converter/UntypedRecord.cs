using System;

namespace Generator.Renderer.Internal.ReturnType;

internal class UntypedRecord : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var type = (GirModel.Record) returnType.AnyTypeReference.AsT0.Type;

        var typeName = returnType switch
        {
            { Transfer: GirModel.Transfer.Full } => Model.UntypedRecord.GetFullyQuallifiedOwnedHandle(type),
            { Transfer: GirModel.Transfer.None } => Model.UntypedRecord.GetFullyQuallifiedUnownedHandle(type),
            _ => throw new Exception($"Unsupported transfer type {returnType.Transfer} for untyped record {type.Name}")
        };

        //Returned SafeHandles are never "null" but "invalid" in case of C NULL.
        return new RenderableReturnType(typeName);
    }
}
