using Generator3.Model;

internal static class GirModelExtension
{
    public static string GetDirection(this GirModel.Parameter parameter, ParameterDirection @in, ParameterDirection @out, ParameterDirection outCallerAllocates, ParameterDirection inout)
    {
        return parameter switch {
            { Direction: GirModel.Direction.InOut } => inout.Value,
            { Direction: GirModel.Direction.Out, CallerAllocates: true } => outCallerAllocates.Value,
            { Direction: GirModel.Direction.Out } => @out.Value,
            _ => @in.Value
        };
    }
    
    public static string GetCanonicalName(this GirModel.Namespace @namespace)
        => $"{@namespace.Name}-{@namespace.Version}";

    public static string GetNativeName(this GirModel.Namespace @namespace)
        => $"{@namespace.Name}.Native";
    
    public static string GetFullyQualifiedNativeRecordStruct(this GirModel.Record record)
        => record.Namespace.GetNativeName() + "." + record.GetName() + ".Struct";
}
