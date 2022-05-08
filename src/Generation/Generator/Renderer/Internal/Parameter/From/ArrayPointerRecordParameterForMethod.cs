using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class ArrayPointerRecordParameterForMethod
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: Type.PointerArray,
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyType.AsT1.Length switch
        {
            { } length => MarshalAs.UnmanagedLpArray(sizeParamIndex: length + 1),
            _ => string.Empty,
        };
    }
}
