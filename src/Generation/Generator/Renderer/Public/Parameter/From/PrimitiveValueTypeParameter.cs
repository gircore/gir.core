using Generator.Model;

namespace Generator.Renderer.Public;

internal static class PrimitiveValueTypeParameter
{
    public static ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0)
        );
    }

    public static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
