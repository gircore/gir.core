using GirModel;
using Transfer = Generator.Model.Transfer;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Utf8String : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Utf8String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        //If ownership is transfered the internal return type is encoded as a string as the
        //marshaller will handle the ownership transfer automatically
        return Transfer.IsOwnedRef(returnType.Transfer)
            ? fromVariableName
            : $"GLib.Internal.StringHelper.ToStringUtf8({fromVariableName})";
    }
}
