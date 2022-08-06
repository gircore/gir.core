using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class InterfaceInstanceParameter
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter)
    {
        return new RenderableInstanceParameter(
            Name: InstanceParameter.GetName(instanceParameter),
            NullableTypeName: Type.Pointer
        );
    }
}
