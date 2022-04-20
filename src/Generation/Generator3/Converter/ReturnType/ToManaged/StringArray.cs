using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class StringArray : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        return returnType.Transfer == Transfer.None && returnType.AnyType.AsT1.Length == null
            ? $"GLib.Internal.StringHelper.ToStringArrayUtf8({fromVariableName})" //variableName is a pointer to a string array 
            : fromVariableName; //variableName is a string[]
    }
}
