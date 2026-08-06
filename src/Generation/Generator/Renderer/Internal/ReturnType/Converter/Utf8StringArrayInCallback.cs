namespace Generator.Renderer.Internal.ReturnType;

internal class Utf8StringArrayInCallback : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyTypeReference.ReferencesArray<GirModel.Utf8String>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var arrayTypeReference = returnType.AnyTypeReference.AsT1;
        var isMarshalAble = returnType.Transfer != GirModel.Transfer.None || arrayTypeReference.Length != null;
        var nullableTypeName = isMarshalAble
            ? Model.ArrayType.GetName(arrayTypeReference)
            : Model.Type.Pointer;

        return new RenderableReturnType(nullableTypeName);
    }
}
