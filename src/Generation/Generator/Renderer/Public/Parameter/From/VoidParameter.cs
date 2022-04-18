using Generator.Model;

namespace Generator.Renderer.Public;

public class VoidParameter
{
    public static ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: string.Empty,
            NullableTypeName: Type.Pointer
        );
    }
}
