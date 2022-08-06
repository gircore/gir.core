using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class RecordInstanceParameter
{
    public static RenderableInstanceParameter Create(GirModel.InstanceParameter instanceParameter)
    {
        var type = (GirModel.Record) instanceParameter.Type;

        return new RenderableInstanceParameter(
            Name: InstanceParameter.GetName(instanceParameter),
            NullableTypeName: Record.GetFullyQualifiedInternalHandle(type)
        );
    }
}
