using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayStringParameterForMethod
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
            { Transfer: GirModel.Transfer.None, AnyType.AsT1.Length: null } => Type.Pointer,
            _ => ArrayType.GetName(parameter.AnyType.AsT1)
        };
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyType.AsT1.Length switch
        {
            null => string.Empty,
            //We add 1 to the length because Methods contain an instance parameter which is not counted
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l + 1)
        };
    }
}
