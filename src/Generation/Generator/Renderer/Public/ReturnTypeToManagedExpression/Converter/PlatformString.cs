using GirModel;
using Transfer = Generator.Model.Transfer;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PlatformString : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.PlatformString>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        var nullCheck = returnType.Nullable
            ? string.Empty
            : " ?? throw new System.Exception(\"Non nullable return type returned a null\")";

        //If ownership is transfered the internal return type is encoded as a string as the
        //marshaller will handle the ownership transfer automatically
        return Transfer.IsOwnedRef(returnType.Transfer)
            ? fromVariableName
            : $"GLib.Internal.StringHelper.ToStringUtf8({fromVariableName}){nullCheck}";
    }
}
