using System.Linq;

namespace Generator.Renderer.Internal;

internal static class Error
{
    public static string Render(GirModel.Callable callable)
    {
        if (!callable.Throws)
            return string.Empty;

        // Add a separator if there are any previous parameters.
        var separator = string.Empty;
        if (callable.Parameters.Any() || callable.InstanceParameter is not null)
            separator = ", ";

        return $"{separator}out GLib.Internal.ErrorOwnedHandle error";
    }

    public static string RenderCallback(GirModel.Callback callback)
    {
        if (!callback.Throws)
            return string.Empty;

        // Add a separator if there are any previous parameters.
        var separator = string.Empty;
        if (callback.Parameters.Any())
            separator = ", ";

        //Callbacks can not marshal SafeHandles. This is why we need to use IntPtr.
        return $"{separator}out IntPtr error";
    }
}
