namespace Generator.Fixer.Callback;

internal class DisableFundamentalReturnTypes : Fixer<GirModel.Callback>
{
    public void Fixup(GirModel.Callback callback)
    {
        if (callback.ReturnType.AnyType.TryPickT0(out var type, out _) && type is GirModel.Class { Fundamental: true })
        {
            /*
             * This Fixer requires other fixers:
             * - Class.PublicMethodsWithCallbackReturnWhichIsFundamental
             * - Record.PublicMethodsWithCallbackReturnWhichIsFundamental
             */
            Model.Callback.Disable(callback);
            Log.Debug($"{callback.Name}: Disabled  because it has a fundamental return type");
        }
    }
}
