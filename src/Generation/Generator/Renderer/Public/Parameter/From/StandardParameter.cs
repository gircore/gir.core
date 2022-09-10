using Generator.Model;

namespace Generator.Renderer.Public;

internal static class StandardParameter
{
    public static ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter) => parameter.AnyType.Match(
        type => Type.GetName(type) + Nullable.Render(parameter),
        arrayType => ArrayType.GetName(arrayType) //TODO: Consider if StandardParameter should support arrays?
    );

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
