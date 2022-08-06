using Generator.Model;

namespace Generator.Renderer.Public;

internal static class RenderableConstantFactory
{
    public static RenderableConstant Create(GirModel.Constant constant)
    {
        return new RenderableConstant(
            Type: Type.GetName(constant.Type),
            Name: Constant.GetName(constant),
            Value: GetValue(constant)
        );
    }

    private static string GetValue(GirModel.Constant constant)
    {
        return constant.Type switch
        {
            GirModel.Bitfield { Name: { } name } => $"({name}) {constant.Value}",
            GirModel.String => "\"" + constant.Value + "\"",
            _ => constant.Value
        };
    }
}
