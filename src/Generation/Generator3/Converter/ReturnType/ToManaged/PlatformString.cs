using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class PlatformString : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.PlatformString>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        //If ownership is transfered the internal return type is encoded as a string as the
        //marshaller will handle the ownership transfer automatically
        return returnType.Transfer.IsOwnedRef()
            ? fromVariableName
            : $"GLib.Internal.StringHelper.ToStringUtf8({fromVariableName})";
    }
}
