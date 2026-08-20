namespace Generator.Renderer.Internal.Parameter;

internal class Bitfield : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.References<GirModel.Bitfield>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            //Internal does not define any bitfields. They are part of the Public API to avoid converting between them.
            NullableTypeName: Model.ComplexType.GetFullyQualified((GirModel.Bitfield) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        { IsPointer: true } => ParameterDirection.Ref(),
        _ => ParameterDirection.In()
    };
}
