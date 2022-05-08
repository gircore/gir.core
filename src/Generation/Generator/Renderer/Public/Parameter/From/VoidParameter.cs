using Generator.Model;

namespace Generator.Renderer.Public;

public class VoidParameter
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Direction: string.Empty,
            NullableTypeName: Type.Pointer,
            Name: Parameter.GetName(parameter)
        );
    }
}
