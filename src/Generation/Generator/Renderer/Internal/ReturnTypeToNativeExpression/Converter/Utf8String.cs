using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Utf8String : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Utf8String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        // Returning a string with transfer=none means that the called function owns the string (e.g. a constant lifetime)
        // This doesn't work since we're returning a newly-allocated UTF-8 string converted from the managed callback's return value.
        if (returnType.Transfer == GirModel.Transfer.None)
            throw new System.NotImplementedException("String return type with transfer=none cannot be converted to native");

        // If transfer=full, return a string that the native caller will own and we do not free.
        return $"GLib.Internal.StringHelper.StringToPtrUtf8({fromVariableName})";
    }
}
