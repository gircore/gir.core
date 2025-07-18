using System;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class InterfaceImplementationSignal
{
    public static string Render(GirModel.Interface iface, GirModel.Signal signal)
    {
        try
        {
            return $@"
#region {Signal.GetName(signal)}
{SignalDescriptor.Render(iface, signal)}
{SignalEvent.Render(iface, signal)}
#endregion
";
        }
        catch (Exception ex)
        {
            var message = $"Did not generate signal '{iface.Name}.{Signal.GetName(signal)}': {ex.Message}";

            if (ex is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }
}
