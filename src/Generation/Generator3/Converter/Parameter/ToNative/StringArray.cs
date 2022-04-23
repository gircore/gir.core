using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class StringArray : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.IsArray<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        var arrayType = parameter.AnyType.AsT1;
        if (parameter.Transfer == Transfer.None && arrayType.Length == null)
        {
            variableName = parameter.GetConvertedName();
            return $"var {variableName} = new GLib.Internal.StringArrayNullTerminatedSafeHandle({parameter.GetPublicName()}).DangerousGetHandle();";
        }

        //We don't need any conversion for string[]
        variableName = parameter.GetPublicName();
        return null;
    }
}
