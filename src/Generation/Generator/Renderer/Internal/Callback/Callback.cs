namespace Generator.Renderer.Internal;

internal static class Callback
{
    public static string Render(GirModel.Callback callback)
    {
        try
        {
            return $"//public delegate {ReturnTypeRendererCallback.Render(callback.ReturnType)} {Model.Callback.GetInternalDelegateName(callback)}({CallbackParameters.Render(callback.Parameters)}{Error.Render(callback)});";
        }
        catch (System.Exception ex)
        {
            Log.Warning($"Could not render internal callback: {callback.Name}: {ex.Message}");
            return string.Empty;
        }
    }
}
