using GirModel;

namespace Generator.Renderer.Public.Constant;

internal class String : ConstantsConverter
{
    public bool Supports(Type type)
    {
        return type is GirModel.String;
    }

    public RenderableConstant Convert(GirModel.Constant constant)
    {
        var typeName = Model.Type.GetName(constant.Type);

        return new RenderableConstant(
            Type: typeName,
            Name: Model.Constant.GetName(constant),
            Value: "\"" + constant.Value + "\""
        );
    }
}
