namespace GirLoader.Output;

public enum Scope
{
    Call,
    Async,
    Notified,
    Forever
}

internal static class ScopeConverter
{
    public static GirModel.Scope? ToGirModel(this Scope? scope) => scope switch
    {
        Scope.Notified => GirModel.Scope.Notified,
        Scope.Async => GirModel.Scope.Async,
        Scope.Call => GirModel.Scope.Call,
        Scope.Forever => GirModel.Scope.Forever,
        _ => null
    };
}
