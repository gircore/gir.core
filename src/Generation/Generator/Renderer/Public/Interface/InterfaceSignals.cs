using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class InterfaceSignals
{
    public static string RenderSignals(this GirModel.Interface @interface)
    {
        return @interface.Signals
            .Select(x => RenderEvent(@interface, x))
            .Join(Environment.NewLine);
    }

    private static string RenderEvent(GirModel.Interface iface, GirModel.Signal signal)
    {
        return $"public event {Signal.GetDelegateName(signal, iface)} {Signal.GetName(signal)};";
    }
}
