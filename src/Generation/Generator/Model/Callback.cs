namespace Generator.Model;

internal static class Callback
{
    public static string GetInternalDelegateName(GirModel.Callback callback)
    {
        return callback.Name.ToPascalCase() + "Callback";
    }

    public static string GetCallHandlerName(GirModel.Callback callback)
    {
        return callback.Name + "CallHandler";
    }

    public static string GetAsyncHandlerName(GirModel.Callback callback)
    {
        return callback.Name + "AsyncHandler";
    }

    public static string GetNotifiedHandlerName(GirModel.Callback callback)
    {
        return callback.Name + "NotifiedHandler";
    }

    public static string GetForeverHandlerName(GirModel.Callback callback)
    {
        return callback.Name + "ForeverHandler";
    }
}
