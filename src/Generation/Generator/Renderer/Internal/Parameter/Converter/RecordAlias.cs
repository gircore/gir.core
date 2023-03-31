namespace Generator.Renderer.Internal.Parameter;

internal class RecordAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsAlias<GirModel.Record>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    //Native records are represented as SafeHandles and are not nullable
    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Record) ((GirModel.Alias) parameter.AnyTypeOrVarArgs.AsT0.AsT0).Type;
        return parameter switch
        {
            { Direction: GirModel.Direction.In } => Model.Record.GetFullyQualifiedInternalHandle(type),
            { CallerAllocates: true } => Model.Record.GetFullyQualifiedInternalHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.Full } => Model.Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.Container } => Model.Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.None } => Model.Record.GetFullyQualifiedInternalUnownedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.Full } => Model.Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.Container } => Model.Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.None } => Model.Record.GetFullyQualifiedInternalUnownedHandle(type),
            _ => throw new System.Exception($"Can't detect parameter type: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
