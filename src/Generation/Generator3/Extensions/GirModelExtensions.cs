using System.Collections.Generic;
using System.Linq;
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

    public static string GetFullyQualifiedInternalStruct(this GirModel.Class @class)
        => @class.Namespace.GetInternalName() + "." + @class.GetName() + ".Instance.Struct";

    public static string GetFullyQualifiedInternalHandle(this GirModel.Record record)
        => record.Namespace.GetInternalName() + "." + record.GetName() + ".Handle";

    public static bool IsUnref(this GirModel.Method method) => method.Name == "unref";
    public static bool IsFree(this GirModel.Method method) => method.Name == "free";
    
    
    public static GirModel.Method? GetFreeOrUnrefMethod(this IEnumerable<GirModel.Method> functions)
        //Unref functions takes precedense over free function
        => functions.FirstOrDefault(function => function.IsUnref()) 
           ?? functions.FirstOrDefault(function => function.IsFree());
    
    public static string GetFullyQualifiedInternalStruct(this GirModel.Union union)
        => union.Namespace.GetInternalName() + "." + union.GetName() + ".Struct";

    public static IEnumerable<GirModel.Parameter> ExceptClosures(this IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
            .Where(p => p.Closure is null or 0)
            .ToList();
    }
}
