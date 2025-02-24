using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class Error
{
    public static string RenderParameter(GirModel.Callable callable)
    {
        if (!callable.Throws)
            return string.Empty;

        // Add a separator if there are any previous parameters.
        var separator = string.Empty;
        if (callable.Parameters.Any() || callable.InstanceParameter is not null)
            separator = ", ";

        return $"{separator}out GLib.Internal.ErrorOwnedHandle error";
    }

    public static string RenderThrowOnError(GirModel.Callable callable)
    {
        return callable.Throws
            ? @"if(!error.IsInvalid)
    throw new GLib.GException(error);"
            : string.Empty;
    }
}
