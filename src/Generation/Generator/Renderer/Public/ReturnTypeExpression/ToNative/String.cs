using GirModel;

namespace Generator.Renderer.Public.ReturnTypeExpression.ToNative;

internal class String : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => returnType.Transfer == Transfer.None
            ? $"GLib.Internal.StringHelper.StringToHGlobalUTF8({fromVariableName})"
            : fromVariableName;
}
