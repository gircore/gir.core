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
}
