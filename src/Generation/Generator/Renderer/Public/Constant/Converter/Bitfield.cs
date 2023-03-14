using GirModel;

namespace Generator.Renderer.Public.Constant;

internal class Bitfield : ConstantsConverter
{
    public bool Supports(Type type)
    {
        return type is GirModel.Bitfield;
    }

    public RenderableConstant Convert(GirModel.Constant constant)
    {
        var typeName = Model.Type.GetName(constant.Type);

        return new RenderableConstant(
            Type: typeName,
            Name: Model.Constant.GetName(constant),
            Value: $"({typeName}) {constant.Value}"
        );
    }
}
