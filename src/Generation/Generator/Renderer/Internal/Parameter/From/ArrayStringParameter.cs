using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayStringParameter
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
            { Transfer: GirModel.Transfer.None, AnyTypeOrVarArgs.AsT0.AsT1.Length: null } => Type.Pointer,
            _ => ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1)
        };
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length switch
        {
            null => string.Empty,
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l)
        };
    }
}
