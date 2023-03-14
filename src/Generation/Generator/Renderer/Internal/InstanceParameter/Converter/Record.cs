namespace Generator.Renderer.Internal.InstanceParameter;

internal class Record : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Record;
    }

    public RenderableInstanceParameter Convert(GirModel.InstanceParameter instanceParameter)
    {
        var type = (GirModel.Record) instanceParameter.Type;

        return new RenderableInstanceParameter(
            Name: Model.InstanceParameter.GetName(instanceParameter),
            NullableTypeName: Model.Record.GetFullyQualifiedInternalHandle(type)
        );
    }
}
