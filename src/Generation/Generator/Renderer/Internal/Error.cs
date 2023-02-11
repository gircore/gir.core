using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

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
}
