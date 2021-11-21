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

    public static string GetInternalName(this GirModel.Namespace @namespace)
        => $"{@namespace.Name}.Internal";
    
    public static string GetFullyQualifiedInternalStruct(this GirModel.Record record)
        => record.Namespace.GetInternalName() + "." + record.GetName() + ".Struct";
    
    public static string GetFullyQualifiedInternalStruct(this GirModel.Union union)
        => union.Namespace.GetInternalName() + "." + union.GetName() + ".Struct";
}
