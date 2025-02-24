namespace Generator.Renderer.Internal.InstanceParameter;

internal class TypedRecord : InstanceParameterConverter
{
    public bool Supports(GirModel.Type type)
    {
        return type is GirModel.Record r && Model.Record.IsTyped(r);
    }

    public RenderableInstanceParameter Convert(GirModel.InstanceParameter instanceParameter)
    {
        return new RenderableInstanceParameter(
            Name: Model.InstanceParameter.GetName(instanceParameter),
            NullableTypeName: GetNullableTypeName(instanceParameter)
        );
    }

    private static string GetNullableTypeName(GirModel.InstanceParameter instanceParameter)
    {
        var type = (GirModel.Record) instanceParameter.Type;
        return instanceParameter switch
        {
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.None } => Model.TypedRecord.GetFullyQuallifiedHandle(type),
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.Full } => Model.TypedRecord.GetFullyQuallifiedUnownedHandle(type),
            _ => throw new System.Exception($"Can't detect typed record instance parameter type {instanceParameter.Name}: CallerAllocates={instanceParameter.CallerAllocates} Direction={instanceParameter.Direction} Transfer={instanceParameter.Transfer}")
        };
    }
}
