namespace Generator.Fixer.Callback;

internal class DisableFundamentalReturnTypes : Fixer<GirModel.Callback>
{
    public void Fixup(GirModel.Callback callback)
    {
        if (callback.ReturnType.AnyTypeReference.TryPickT0(out var typeReference, out _) && typeReference.Type is GirModel.Class { Fundamental: true })
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
