using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public;

internal static class DocComments
{
    public static string Render(IEnumerable<ParameterToNativeData> parameters)
    {
        return parameters
            .Where(x => !x.IsDestroyNotify)
            .Select(x => x.Parameter)
            .Select(Render)
            .OfType<string>()
            .Join(Environment.NewLine);
    }

    private static string? Render(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            { AnyTypeOrVarArgs.IsT1: true } => null, //varargs
            { AnyTypeOrVarArgs.AsT0.IsT1: true } => null, //array
            { AnyTypeOrVarArgs.AsT0.AsT0: GirModel.Callback }
                => $"""/// <param name="{Model.Parameter.GetName(parameter)}">A callback. If it raises an exception the application will terminate. To receive this unhandled exception see <see cref="GLib.UnhandledException.SetHandler"/>.</param>""",
            _ => null
        };
    }
}
