namespace Generator.Renderer.Public.Parameter;

internal class Pointer : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.References<GirModel.Pointer>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
