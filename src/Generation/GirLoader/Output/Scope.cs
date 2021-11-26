namespace GirLoader.Output
{
    public enum Scope
    {
        Call,
        Async,
        Notified
    }
    
    internal static class ScopeConverter
    {
        public static GirModel.Scope ToGirModel(this Scope scope) => scope switch
        {
            Scope.Notified => GirModel.Scope.Notified,
            Scope.Async => GirModel.Scope.Async,
            _ => GirModel.Scope.Call
        };
    }
}
