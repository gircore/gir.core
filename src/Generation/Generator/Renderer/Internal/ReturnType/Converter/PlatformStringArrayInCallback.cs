namespace Generator.Renderer.Internal.ReturnType;

internal class PlatformStringArrayInCallback : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsArray<GirModel.PlatformString>();
    }

    public RenderableReturnType Convert(GirModel.ReturnType returnType)
    {
        var arrayType = returnType.AnyType.AsT1;
        var isMarshalAble = returnType.Transfer != GirModel.Transfer.None || arrayType.Length != null;
        var nullableTypeName = isMarshalAble
            ? Model.ArrayType.GetName(arrayType)
            : Model.Type.Pointer;

        return new RenderableReturnType(nullableTypeName);
    }
}
